using Microsoft.Win32;
using System;
using System.IO;

namespace Insomnia
{
    public static class Autorun
    {
        private const string KeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public static void Add()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyPath, true);
            string dir = Environment.CurrentDirectory;
            string path = Path.Combine(dir, $"{Program.Name}.exe");

            key?.SetValue(Program.Name, path);
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
