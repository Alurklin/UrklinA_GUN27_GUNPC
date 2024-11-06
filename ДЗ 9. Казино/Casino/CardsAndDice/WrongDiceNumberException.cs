using System;

namespace CasinoGame.CardsAndDice
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int min, int max)
            : base($"Invalid dice number: {min}. Valid range is 1 to {max}.")
        {
        }
    }
}

