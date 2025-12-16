using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
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
    private int handsRemaining = 1000;
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
        string result = await client.GetStringAsync("https://deckofcardsapi.com/api/deck/new/"); // Hämtar ett shuffled deck :)
        deck = JsonSerializer.Deserialize<Deck>(result)!;
    }

    private async Task StartRound()
    {
        Hand hand = new();
        await hand.FirstDraw(deck, client);

        Card sCard = new();
        sCard.suit = "Special";
        sCard.value = "?";
        deck.AddCards(sCard);
        deck.AddCards(sCard);
        deck.AddCards(sCard);
        deck.AddCards(sCard);
        deck.AddCards(sCard);
        deck.AddCards(sCard);
        deck.AddCards(sCard);

        // Game Loop
        while (handsRemaining > 0)
        {
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


    public enum HandType
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush
    }

    private HandType DetectHand(Card[] playedHand)
    {
        bool flush = IsFlush(playedHand);
        bool straight = IsStraight(playedHand);

        if (straight && flush) return HandType.StraightFlush;
        if (straight) return HandType.Straight;
        if (flush) return HandType.Flush;

        return HandType.HighCard;
    }

    private int ScoreHand(Card[] playedHand)
    {
        // if (playedHand == null || playedHand.Length == 0) return 0;
        // int total = 0;
        // Dictionary<int, int> counts = new();

        // foreach (Card c in playedHand)
        // {
        //     int value = c.GetValue();
        //     if (counts.ContainsKey(value))
        //         counts[value]++;
        //     else
        //         counts[value] = 1;
        // }

        // if (IsFlush(playedHand)) Console.WriteLine("Flush!"); ;
        // if (IsStraight(playedHand)) Console.WriteLine("Straight!");

        // if (counts.All(k => k.Value == 1))
        //     return counts.Keys.Max();
        HandType type = DetectHand(playedHand);

        return type switch
        {
            HandType.Straight => 50,
            HandType.Flush => 60,
            HandType.StraightFlush => 80,
            HandType.HighCard => 10,
            _ => 0
        };
    }


    // Hämta antallet "duplicates" för att checka pairs etc
    private Dictionary<int, int> GetCount()
    {
        return new();
    }

    private bool IsFlush(Card[] playedCards)
    {
        if (playedCards.Length != 5) return false;
        return playedCards.All(c => c.suit == playedCards[0].suit);
    }

    private bool IsStraight(Card[] playedCards)
    {
        int[] values = playedCards.Select(c => c.GetValue()).ToArray();
        if (values.Length != 5) return false;

        values = values.Distinct().OrderBy(v => v).ToArray();
        if (values.Length != 5) return false;

        bool normalStraight = values[4] - values[0] == 4;

        return normalStraight;
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
                score += ScoreHand(hand);
                handsRemaining--;
                break;
            default:
                Console.WriteLine("Discarded");
                discardsRemaining--;
                break;
        }


    }
}
