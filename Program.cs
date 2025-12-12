using System.Text.Json;

HttpClient client = new();
string result = await client.GetStringAsync("https://deckofcardsapi.com/api/deck/new/shuffle/?jokers_enabled=true");

Console.WriteLine(result);
Deck deck = JsonSerializer.Deserialize<Deck>(result)!;
Console.WriteLine(deck.deck_id);
Console.WriteLine(deck.Count);

DrawResponse draw = await deck.DrawCards(1, client);
Console.WriteLine($"{draw.cards[0].value} of {draw.cards[0].suit}");

class Deck
{
    public string deck_id { get; set; }
    public int remaining { private get; set; }

    public async Task<DrawResponse> DrawCards(int amount, HttpClient client)
    {
        if (amount < 0) return new DrawResponse();
        string url = $"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count={amount}";
        string json = await client.GetStringAsync(url);

        return JsonSerializer.Deserialize<DrawResponse>(json);
    }

    public int Count => remaining;
}

class DrawResponse
{
    public bool success { get; set; }
    public int remaining { get; set; }
    public Card[] cards { get; set; }
}

class Card
{
    public string code { get; set; }
    public int value { get; set; }
    public string suit { get; set; }
}

// class Card
// {
//     public Suit Suit;
//     public Rank Rank;

//     public virtual int Value => (int)Rank;

//     public virtual float GetMultiplier()
//     {
//         return 1;
//     }

//     public Card(Suit suit, Rank rank)
//     {
//         Suit = suit;
//         Rank = rank;
//     }

//     public override string ToString() => $"{Suit} of {Rank} {Value}";
// }
// class Joker : Card
// {
//     public Joker() : base(Suit.Spade, Rank.Ace)
//     {

//     }

//     public override int Value => 100;

//     public override float GetMultiplier()
//     {
//         return 2;
//     }

//     public override string ToString() => "Joker";
// }
