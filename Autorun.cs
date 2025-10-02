using Microsoft.Win32;

namespace Insomnia
{
    public static class Autorun
    {
        private const string KeyPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static void Add()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyPath, true);
            string path = Program.ProcessPath;

            key?.SetValue(Program.Name, $"\"{path}\" {Program.SilentArgument}");
        }

        public static void Remove()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyPath, true);
            key?.DeleteValue(Program.Name, false);
        }

        public static bool Exists()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyPath, false);
            return key?.GetValue(Program.Name) != null;
        }
    }
}
