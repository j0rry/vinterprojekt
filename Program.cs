using System.Text.Json;

HttpClient client = new();
string result = await client.GetStringAsync("https://deckofcardsapi.com/api/deck/new/shuffle/?jokers_enabled=true"); // Hämtar ett nytt deck med 2 jokerar

Deck deck = JsonSerializer.Deserialize<Deck>(result)!;
// Console.WriteLine(deck.deck_id);
// Console.WriteLine(deck.Count);

// Drar 5 kort och visar dem
DrawResponse draw = await deck.DrawCardsAsync(8, client);
if (draw.cards == null) return;
foreach (Card card in draw.cards)
{
    Console.WriteLine(card);
}

List<String[]> asciiCards = draw.cards.Select(c => c.GetAscii()).ToList();
// Print 5 lines (the height of each ASCII card)
for (int line = 0; line < asciiCards[0].Length; line++)
{
    foreach (var card in asciiCards)
    {
        Console.Write(card[line] + "  ");   // two spaces between cards
    }
    Console.WriteLine();
}
