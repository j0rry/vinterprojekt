using System.Text.Json;

HttpClient client = new();
string result = await client.GetStringAsync("https://deckofcardsapi.com/api/deck/new/shuffle/?jokers_enabled=true"); // Hämtar ett nytt deck med 2 jokerar

Deck deck = JsonSerializer.Deserialize<Deck>(result)!;
Console.WriteLine(deck.deck_id);
Console.WriteLine(deck.Count);

// Drar 5 kort och visar dem
DrawResponse draw = await deck.DrawCardsAsync(5, client);
foreach (Card card in draw.cards)
{
    Console.WriteLine(card.value);
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
