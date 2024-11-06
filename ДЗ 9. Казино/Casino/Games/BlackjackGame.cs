using System;
using System.Collections.Generic;
using CasinoGame.CardsAndDice;

namespace CasinoGame.Games
{
    public class BlackjackGame : CasinoGameBase
    {
        private Queue<Card> _deck;

        public BlackjackGame(int cardCount)
        {
            _deck = new Queue<Card>();
            FactoryMethod();
            Shuffle();
        }

        protected override void FactoryMethod()
        {
            // Инициализация колоды карт с определенным количеством карт
        }

        private void Shuffle()
        {
            // Перемешивание и добавление карт в очередь _deck
        }

        public override void PlayGame()
        {
            // Логика игры в Блэкджек
        }
    }
}

