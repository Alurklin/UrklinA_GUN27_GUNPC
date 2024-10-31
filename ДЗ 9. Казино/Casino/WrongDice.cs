using System;

namespace Casino
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int number, int min, int max)
            : base($"Number {number} is out of range ({min}-{max})")
        {
        }
    }
}
