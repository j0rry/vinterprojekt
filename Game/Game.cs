using System.Text.Json;

public class Game
{
    private Deck deck = new();
    private HttpClient client;
    private int money;
    private int round = 1;
    private int blind = 500;
    private int score;

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

        Card sCard = new();
        sCard.suit = "Special";
        sCard.value = "100";
        deck.AddCards(sCard);

        // Game Loop
        while (handsRemaining > 0)
        {
            if (!test) Console.Clear();
            Console.WriteLine($"Deck ID: {deck.ID} \n");
            Console.WriteLine($"<ROUND> {round}");
            Console.WriteLine($" <Score> {score} / {blind}");
            Console.WriteLine($" <Remaining Cards>: {deck.TotalRemaining}");
            Console.WriteLine($" <Hands left>: {handsRemaining}\n Discards left: {discardsRemaining} \n");
            Console.WriteLine($" <Money>: {money}\n");
            UI.ShowHand(hand.Cards.ToArray());

            Card[] playCards = hand.SelectCards().ToArray();
            HandOptions(playCards);
            hand.RemoveCards(playCards.ToList());
            await hand.DrawToFullAsync(deck, client);

            // switch (numChoice)
            // {
            //     case 1:
            //         List<Card> playCards = hand.SelectCards();
            //         foreach (Card c in playCards)
            //             Console.WriteLine(c);
            //         hand.RemoveCards(playCards);
            //         score = ValidateCards(playCards.ToArray());
            //         hand.DrawToFull(deck);
            //         test = true;
            //         break;
            //     default:
            //         Console.WriteLine("Invalid choice!");
            //         test = false;
            //         break;
            // }
        }
    }

    private int ValidateCards(Card[] playedHand)
    {
        if (playedHand == null || playedHand.Length == 0) return 0;
        int total = 0;
        Dictionary<int, int> counts = new();

        foreach (Card c in playedHand)
        {
            int value = c.GetValue();
            if (counts.ContainsKey(value))
                counts[value]++;
            else
                counts[value] = 1;
        }

        if (counts.All(kvp => kvp.Value == 1))
            return counts.Keys.Max();

        foreach (var kvp in counts)
        {
            int value = kvp.Key;
            int count = kvp.Value;

            switch (count)
            {
                case 1:
                    total += value;
                    break;
                case 2:
                    total += value * 2 + 5;
                    break;
                case 3:
                    total += value * 3 + 10;
                    break;
                case 4:
                    total += value * 4 + 20;
                    break;
            }
        }

        return total;
    }

    private void HandOptions(Card[] hand)
    {
        Console.WriteLine("[1] Use Hand [2] Discard Hand");
        Console.Write("> ");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
        {
            Console.WriteLine("1 or 2 :D");
        }

        switch (choice)
        {
            case 1:
                score += ValidateCards(hand);
                handsRemaining--;
                break;
            default:
                Console.WriteLine("Discarded");
                discardsRemaining--;
                break;
        }


    }
}
