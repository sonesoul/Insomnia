using Insomnia.AppWindow.Elements;

namespace Insomnia.AppWindow
{
    public class UIDrawer
    {
        private IUIElement[] _elements;

        public UIDrawer(IUIElement[] elements)
        {
            _elements = elements;
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