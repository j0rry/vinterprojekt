
Game g = new();
await g.StartAsync();



// HttpClient client = new();
// string result = await client.GetStringAsync("https://deckofcardsapi.com/api/deck/new/shuffle/?jokers_enabled=true"); // Hämtar ett nytt deck med 2 jokerar

// Deck deck = JsonSerializer.Deserialize<Deck>(result)!;
// // Console.WriteLine(deck.deck_id);
// // Console.WriteLine(deck.Count);

// // Drar 5 kort och visar dem
// DrawResponse draw = await deck.DrawCardsAsync(8, client);
// if (draw.cards == null) return;
// foreach (Card card in draw.cards)
// {
//     Console.WriteLine(card);
// }

