using System;
using SDL3;
using static SDL3.SDL;

namespace Insomnia.DirectMedia.Types
{
    public class Renderer : Resource
    {
        public Color Color => _color;
        
        private Color _color;

        public Renderer(IntPtr handle)
        {
            Pointer = CreateRenderer(handle, null);
        }

        public void SetColor(Color c)
        {
            SetRenderDrawColor(
                this, 
                c.Red, 
                c.Green, 
                c.Blue, 
                c.Alpha);

            _color = c;
        }
        public void Clear(Color color)
        {
            Color temp = Color;
            SetColor(color);
            RenderClear(this);
            SetColor(temp);
        }

        public void SetTarget(IntPtr texture)
        {
            SetRenderTarget(this, texture);
        }
        public void UnsetTarget()
        {
            SetTarget(IntPtr.Zero);
        }

        protected override void Destroy()
        {
            DestroyRenderer(this);
        }
    }
}