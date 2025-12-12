using System.Text.Json;

public class Game
{
    private Deck deck = new();
    private HttpClient client;
    private int money;
    private int round = 1;

    bool test = false;
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
            if (!test) Console.Clear();
            Console.WriteLine($"Round {round}");
            Console.WriteLine($"Remaining Cards: {deck.Count}");
            Console.WriteLine($"Deck ID: {deck.ID}");
            UI.ShowHand(hand.Cards.ToArray());

            int numChoice;
            ShowOptions();
            Console.Write("> ");
            while (!int.TryParse(Console.ReadLine(), out numChoice)) Console.WriteLine("A number Please!");

            switch (numChoice)
            {
                case 1:
                    List<Card> playCards = hand.SelectCards();
                    foreach (Card c in playCards)
                        Console.WriteLine(c);
                    hand.RemoveCards(playCards);
                    hand.DrawToFull(deck);
                    test = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    test = false;
                    break;
            }
        }
    }

    private void ShowOptions()
    {
        Console.WriteLine("[1] Select Cards");
    }
}
