using System.Collections.Generic;

namespace Casino
{
    public class DiceGame : CasinoGameBase
    {
        private List<Dice> _dice;

        public DiceGame(int numberOfDice, int min, int max)
        {
            _dice = new List<Dice>();
            for (int i = 0; i < numberOfDice; i++)
            {
                _dice.Add(new Dice(min, max));
            }
        }

        public override void PlayGame()
        {
            // Подсчёт очков для игрока и компьютера
            int playerScore = 0;
            int computerScore = 0;

            foreach (var die in _dice)
            {
                playerScore += die.Number;
                computerScore += die.Number;
            }

            if (playerScore > computerScore) OnWinInvoke();
            else if (playerScore < computerScore) OnLooseInvoke();
            else OnDrawInvoke();
        }

        protected override void FactoryMethod() { }
    }
}

