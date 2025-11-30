Deck deck = new();

while (true)
{
    Console.WriteLine(deck.DrawCard().ToString());
    Console.ReadLine();
}

public enum Suit
{
    Heart,
    Diamond,
    Club,
    Spade
}

public enum Rank
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

class Card
{
    public Suit Suit;
    public Rank Rank;

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString() => $"{Suit} of {Rank}";
}

class Deck
{
    //    public List<Card> cards;

    private Stack<Card> _cards;

    public Deck(int decks = 1)
    {
        _cards = new(52 * decks);
        Build(decks);
    }

    private void Build(int decks)
    {
        _cards.Clear();

        for (int d = 0; d < decks; d++)
        {
            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Push(new Card(s, r));
                }
            }
        }
    }

    public Card DrawCard()
    {
        return _cards.Pop();
    }
}
