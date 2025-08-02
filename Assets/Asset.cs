using System;
using System.IO;

namespace Insomnia.Assets
{
    public class Asset
    {
        public string AbsolutePath { get; set; }
        public string RelativePath { get; private set; }
        public static string ContentDirectory => Path.Combine(Environment.CurrentDirectory, "Content\\");

        public Asset(string relativePath)
        {
            RelativePath = relativePath;
            AbsolutePath = GetAbsolutePath(relativePath);
        }

        public static string GetAbsolutePath(string relativePath) => Path.Combine(ContentDirectory, relativePath);
    }
}