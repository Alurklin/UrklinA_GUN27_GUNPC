using CasinoGame.CardsAndDice;
using System;
using System.Collections.Generic;

namespace CasinoGame.Games
{
    public class DiceGame : CasinoGameBase
    {
        private List<Dice> _diceList;

        public DiceGame(int diceCount, int minValue, int maxValue)
        {
            _diceList = new List<Dice>();

            for (int i = 0; i < diceCount; i++)
            {
                _diceList.Add(new Dice(minValue, maxValue));
            }
        }

        protected override void FactoryMethod()
        {
            // Можно оставить пустым, так как кости уже создаются в конструкторе
        }

        public override void PlayGame()
        {
            int playerTotal = 0;
            int computerTotal = 0;

            // Подсчёт суммы для игрока
            foreach (var dice in _diceList)
            {
                playerTotal += dice.Number; // Используем свойство Number для генерации броска
            }

            // Подсчёт суммы для компьютера
            foreach (var dice in _diceList)
            {
                computerTotal += dice.Number; // Используем свойство Number для генерации броска
            }

            // Логика победы, ничьей и проигрыша
            if (playerTotal > computerTotal)
            {
                OnWinInvoke();  // Вызов события победы
            }
            else if (playerTotal < computerTotal)
            {
                OnLoseInvoke();  // Вызов события поражения
            }
            else
            {
                OnDrawInvoke();  // Вызов события ничьей
            }
        }

    }
}
