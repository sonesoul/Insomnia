using static SDL3.SDL;

namespace Insomnia.DirectMedia
{
    public static class Mouse
    {
        public static Vector2 Position => GetGlobalState().position;
        public static MouseButtonFlags Flags => GetGlobalState().flags;

        public static Vector2 GetRelativePosition(Window window)
        {
            Vector2 mousePos = Position;
            Vector2 windowPos = window.Position.ToVector2();

            return mousePos - windowPos;
        }

        private static (MouseButtonFlags flags, Vector2 position) GetGlobalState()
        {
            MouseButtonFlags flags = GetGlobalMouseState(out float x, out float y);

            return (flags, new Vector2(x, y));
        }
    }
}