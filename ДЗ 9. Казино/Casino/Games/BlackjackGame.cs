using System;
using System.Collections.Generic;
using CasinoGame.CardsAndDice;

namespace CasinoGame.Games
{
    public class BlackjackGame : CasinoGameBase
    {
        private Queue<Card> _deck;
        private List<Card> _playerHand;
        private List<Card> _dealerHand;
        private const int MaxScore = 21;
        private int _deckCount;

        public BlackjackGame(int deckCount)
        {
            _deck = new Queue<Card>();
            _playerHand = new List<Card>();
            _dealerHand = new List<Card>();
            _deckCount = deckCount;
            FactoryMethod();
            Shuffle();
        }

        protected override void FactoryMethod()
        {
            for (int i = 0; i < _deckCount; i++)
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    foreach (CardValue rank in Enum.GetValues(typeof(CardValue)))
                    {
                        _deck.Enqueue(new Card(suit, rank));
                    }
                }
            }
        }

        private void Shuffle()
        {
            var cards = new List<Card>(_deck);
            var random = new Random();
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
            _deck = new Queue<Card>(cards);
        }

        public override void PlayGame()
        {
            // Пример получения очков у карт в руке
            int playerScore = CalculateHandScore(_playerHand);
            int dealerScore = CalculateHandScore(_dealerHand);

            // Продолжение логики игры
        }

        private int CalculateHandScore(List<Card> hand)
        {
            int score = 0;
            foreach (var card in hand)
            {
                score += card.GetCardValue();
            }
            return score;
        }
    }
}
