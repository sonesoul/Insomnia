namespace Insomnia.Coroutines
{
    public static class Time
    {
        public static float Delta { get; private set; } = 0.16f;
        public static ulong LastTicks { get; private set; } 

        public static void Update()
        {
            ulong currentTicks = Now();
            Delta = (currentTicks - LastTicks)/ 1000;
            LastTicks = currentTicks;
        }

        public static ulong Now() => SDL3.SDL.GetTicks();
    }
}