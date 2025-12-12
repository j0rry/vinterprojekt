using System.Text.Json;

public class Game
{
    private Deck deck = new();
    private HttpClient client;
    private int money;
    private int round = 1;

    public Game()
    {
        client = new();
    }

    public async Task StartAsync()
    {
        await LoadDeck();
        await StartRound();
    }

    private async Task LoadDeck()
    {
        string result = await client.GetStringAsync("https://deckofcardsapi.com/api/deck/new/shuffle/"); // Hämtar ett nytt deck med 2 jokerar
        deck = JsonSerializer.Deserialize<Deck>(result)!;
    }

    private async Task StartRound()
    {
        Console.WriteLine($"Round {round}");

        DrawResponse draw = await deck.DrawCardsAsync(8, client);
        if (draw.cards == null) return;
        deck.remaining = draw.remaining;
        Console.WriteLine($"Remaining Cards: {deck.Count}");

        List<String[]> asciiCards = draw.cards.Select(c => c.GetAscii()).ToList();
        for (int line = 0; line < asciiCards[0].Length; line++)
        {
            foreach (var card in asciiCards)
            {
                Console.Write(card[line] + "  ");
            }
            Console.WriteLine();
        }

    }
}
