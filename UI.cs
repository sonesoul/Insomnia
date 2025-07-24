using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia
{
    public interface IUIElement
    {
        public void Draw(float dt);
    }

    public class UIDrawer
    {
        private IUIElement[] _elements;

        public UIDrawer(Window window, IUIElement[] elements)
        {
            _elements = elements;
            window.Draw += Draw;
        }
        
        public void Draw(float dt)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].Draw(dt);
            }
        }
    }

    
}
