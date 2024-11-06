using System.Collections.Generic;
using CasinoGame.CardsAndDice;

namespace CasinoGame.Games
{
    public class DiceGame : CasinoGameBase
    {
        private readonly List<Dice> _dices;

        public DiceGame(int diceCount, int minValue, int maxValue)
        {
            _dices = new List<Dice>();
            for (int i = 0; i < diceCount; i++)
            {
                _dices.Add(new Dice(minValue, maxValue));
            }
        }

        protected override void FactoryMethod() { /* Создание костей */ }

        public override void PlayGame()
        {
            // Логика игры в кости
        }
    }
}

