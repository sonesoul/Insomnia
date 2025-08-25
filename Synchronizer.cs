using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Insomnia
{
    public class Synchronizer
    {
        public const string Name = $"{nameof(Insomnia)}";
        public const string PipeName = $"{nameof(Insomnia)}Pipe";

        public Mutex Mutex { get; private set; } 
        public StreamReader StreamReader { get; private set;  }

        private NamedPipeServerStream _server;

        public Synchronizer(out bool createdNew)
        {
            Mutex = new(false, Name, out createdNew);
            
            if (!createdNew)
            {
                WakeUpInstance();
                return;
            }

            CreateServer();
        }

        private void CreateServer()
        {
            _server = new(PipeName);
            StreamReader = new StreamReader(_server);

            Task.Run(WaitForConnections);
        }
        private void WakeUpInstance()
        {
            using var client = new NamedPipeClientStream(".", PipeName, PipeDirection.Out);
            client.Connect();
        }

        private async void WaitForConnections()
        {
            while (true)
            {
                if (_server.IsConnected)
                {
                    _server.Disconnect();
                }

                await _server.WaitForConnectionAsync();
                var window = Program.MainWindow.Window;
                window.IsVisible = !window.IsVisible;

            }
        }
    }
}