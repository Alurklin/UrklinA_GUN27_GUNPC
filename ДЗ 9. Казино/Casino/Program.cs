using System;

namespace Casino
{
    class Program
    {
        static void Main(string[] args)
        {
            var casino = new Casino("C:\\CasinoProfiles");
            casino.StartGame();
        }
    }
}