using System;
using System.IO;

namespace Insomnia.Assets
{
    public class Asset(string relativePath)
    {
        public string AbsolutePath { get; set; } = GetAbsolutePath(relativePath);
        public string RelativePath { get; private set; } = relativePath;
        public static string ContentDirectory => Path.Combine(Environment.CurrentDirectory, "Content\\");

        public static string GetAbsolutePath(string relativePath) => Path.Combine(ContentDirectory, relativePath);
    }
}