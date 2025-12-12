using System.Text.Json;

HttpClient client = new();
string result = await client.GetStringAsync("https://deckofcardsapi.com/api/deck/new/shuffle/?jokers_enabled=true");

Console.WriteLine(result);
Deck deck = JsonSerializer.Deserialize<Deck>(result)!;
Console.WriteLine(deck.deck_id);
Console.WriteLine(deck.remaining);

public enum Suit
{
    Heart,
    Diamond,
    Club,
    Spade,
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

class Joker : Card
{
    public Joker() : base(Suit.Spade, Rank.Ace)
    {

    }

    public override int Value => 100;

    public override float GetMultiplier()
    {
        return 2;
    }

    public override string ToString() => "Joker";
}

class Card
{
    public Suit Suit;
    public Rank Rank;

    public virtual int Value => (int)Rank;

    public virtual float GetMultiplier()
    {
        return 1;
    }

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
    public string deck_id { get; set; }
    public int remaining { get; set; }

    // private Stack<Card> _cards;

    // public Deck(int decks = 1)
    // {
    //     _cards = new(52 * decks);
    //     Build(decks);
    //     Shuffle();
    // }

    // private void Build(int decks)
    // {
    //     _cards.Clear();

    //     for (int d = 0; d < decks; d++)
    //     {
    //         foreach (Suit s in Enum.GetValues(typeof(Suit)))
    //         {
    //             foreach (Rank r in Enum.GetValues(typeof(Rank)))
    //             {
    //                 _cards.Push(new Card(s, r));
    //             }
    //         }
    //     }

    //     _cards.Push(new Joker());
    //     _cards.Push(new Joker());
    // }

    // private void Shuffle()
    // {
    //     _cards = new Stack<Card>(_cards.OrderBy(card => Random.Shared.Next()));
    // }

    // public Card DrawTop()
    // {
    //     return _cards.Pop();
    // }

    // public void AddCard(Card cardToAdd)
    // {
    //     _cards.Push(cardToAdd);
    // }

    public int Count => remaining;
}
