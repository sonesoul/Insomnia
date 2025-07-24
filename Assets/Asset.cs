using System;
using System.IO;

namespace Insomnia.Assets
{
    public abstract class Asset<T>
    {
        public static string ContentDirectory => Path.Combine(Environment.CurrentDirectory, "Content\\");

        public string RelativePath { get; private set; }
        public T Data { get; protected set; }

        protected Asset(string relativePath) => RelativePath = relativePath;

        public static string GetAbsolutePath(string relativePath) => Path.Combine(ContentDirectory, relativePath);
    }
}