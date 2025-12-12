using System.Text.Json;

public class Game
{
    private Deck deck = new();
    private HttpClient client;
    private int money;
    private int round = 1;

    private int handsRemaining = 3;
    private int discardsRemaining = 3;

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
        string result = await client.GetStringAsync("https://deckofcardsapi.com/api/deck/new/shuffle/"); // Hämtar ett shuffled deck :)
        deck = JsonSerializer.Deserialize<Deck>(result)!;
    }

    private async Task StartRound()
    {

        Hand hand = new();
        await hand.FirstDraw(deck, client);

        // Game Loop
        while (handsRemaining > 0)
        {
            Console.WriteLine($"Round {round}");
            Console.WriteLine($"Remaining Cards: {deck.Count}");
            UI.ShowHand(hand.Cards.ToArray());

            Console.ReadLine();
        }


    }
}
