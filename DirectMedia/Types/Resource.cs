using System;

namespace Insomnia.DirectMedia.Types
{
    public abstract class Resource : IDisposable
    {
        public IntPtr Pointer { get; protected set; }
        public bool IsDisposed { get; private set; } = false;

        public void Dispose()
        {
            if (IsDisposed) 
                return;

            IsDisposed = true;

            Destroy();
            GC.SuppressFinalize(this);
        }

        protected abstract void Destroy();

        public static implicit operator IntPtr(Resource r) => r.Pointer;
    }
}
