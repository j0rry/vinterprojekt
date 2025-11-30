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
}

class Deck
{
    public List<Card> cards;

    public Deck(int decks = 1)
    {
        cards = new(52 * decks);
    }
}
