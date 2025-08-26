using System;
using System.IO;

namespace Insomnia
{
    public class Settings
    {
        public bool IsKeeperActive { get; set; }
        public TimeSpan IdleThreshold { get; set; }

        public string FilePath { get; } = $"{Path.Combine(Environment.CurrentDirectory, Name)}";
        public const string Name = "settings.txt";
        public const char SplitSymbol = '=';
        public Settings()
        {
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Dispose();
                IsKeeperActive = true;
                IdleThreshold = TimeSpan.FromMinutes(1);
                SaveState();
            }
        }

        public void SaveState()
        {
            string[] lines = [
                WriteSetting("IsWorking", $"{IsKeeperActive}"),
                WriteSetting("IdleThresholdSeconds", $"{IdleThreshold.TotalSeconds}"),
            ];

            File.WriteAllLines(FilePath, lines);
        }
        public void LoadState() 
        {
            string[] lines = File.ReadAllLines(FilePath);

            IsKeeperActive = bool.Parse(GetValue(lines[0]));
            IdleThreshold = TimeSpan.FromSeconds(int.Parse(GetValue(lines[1])));
        }

        private static string GetValue(string line)
        {
            var parts = line.Split(SplitSymbol);
            return parts[1];
        }
        private static string WriteSetting(string name, string value)
        {
            return $"{name}{SplitSymbol}{value}";
        }
    }
}