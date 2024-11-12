namespace CasinoGame.CardsAndDice
{
    public struct Card
    {
        public CardSuit Suit { get; }
        public CardValue Rank { get; }

        public Card(CardSuit suit, CardValue rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public int GetCardValue()
        {
            switch (Rank)
            {
                case CardValue.Jack:
                case CardValue.Queen:
                case CardValue.King:
                    return 10;
                case CardValue.Ace:
                    return 11; // Или 1, в зависимости от правил
                default:
                    return (int)Rank;
            }
        }
    }
}
