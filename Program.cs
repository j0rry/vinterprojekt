Deck deck = new();

while (true)
{
    Console.WriteLine(deck.DrawCard().ToString());
    Console.ReadLine();
}

// Tillfälligt använder enums. Måste enklare kunna spara ner värde på korten.
public enum Suit
{
    Heart,
    Diamond,
    Club,
    Spade
}

public enum Rank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}

class Card
{
    public Suit Suit;
    public Rank Rank;

    public int Value => (int)Rank;

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString() => $"{Suit} of {Rank} {Value}";
}

class Deck
{
    //    public List<Card> cards;

    private Stack<Card> _cards;

    public Deck(int decks = 1)
    {
        _cards = new(52 * decks);
        Build(decks);
        Shuffle();
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

    private void Shuffle()
    {

    }

    public Card DrawCard()
    {
        return _cards.Pop();
    }

    public void AddCard(Card cardToAdd)
    {
        _cards.Push(cardToAdd);
    }

    public int Count => _cards.Count;
}
