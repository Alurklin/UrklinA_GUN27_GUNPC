using System.Collections.Generic;

namespace Casino
{
    public class Blackjack : CasinoGameBase
    {
        private Queue<Card> _deck;
        private List<Card> _playerCards;
        private List<Card> _computerCards;

        public Blackjack()
        {
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            _deck = new Queue<Card>();
            Shuffle();
        }

        private void Shuffle()
        {
            // Перемешивание колоды (пока упрощённый пример)
            foreach (Suit suit in (Suit[])System.Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in (Rank[])System.Enum.GetValues(typeof(Rank)))
                {
                    _deck.Enqueue(new Card(suit, rank));
                }
            }
        }

        public override void PlayGame()
        {
            // Логика игры в блэкджек
            _playerCards = new List<Card> { _deck.Dequeue(), _deck.Dequeue() };
            _computerCards = new List<Card> { _deck.Dequeue(), _deck.Dequeue() };

            // Подсчёт очков и определение победителя (упрощённый пример)
            int playerScore = CalculateScore(_playerCards);
            int computerScore = CalculateScore(_computerCards);

            if (playerScore > 21) OnLooseInvoke();
            else if (computerScore > 21 || playerScore > computerScore) OnWinInvoke();
            else if (playerScore < computerScore) OnLooseInvoke();
            else OnDrawInvoke();
        }

        private int CalculateScore(List<Card> cards)
        {
            int score = 0;
            foreach (var card in cards)
            {
                score += (int)card.Rank;
            }
            return score;
        }
    }
}
