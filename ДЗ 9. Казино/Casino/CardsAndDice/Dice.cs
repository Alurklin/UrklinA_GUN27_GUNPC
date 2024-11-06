using System;

namespace CasinoGame.CardsAndDice
{
    public struct Dice
    {
        private readonly int _min;
        private readonly int _max;

        public int Number => new Random().Next(_min, _max + 1);

        public Dice(int min, int max)
        {
            if (min < 1 || max > int.MaxValue)
            {
                throw new WrongDiceNumberException(min, max);
            }

            _min = min;
            _max = max;
        }
    }
}
