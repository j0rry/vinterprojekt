using System.Runtime.CompilerServices;
using System.Text.Json;

public class Deck
{
    public string? deck_id { get; set; }
    public int remainingApiCards;

    public int TotalRemaining => remainingApiCards + extraCards.Count;

    private readonly Stack<Card> extraCards = new();

    public async Task<DrawResponse> DrawFromApiAsync(int amount, HttpClient client)
    {
        string url = $"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count={amount}";
        string json = await client.GetStringAsync(url);

        return JsonSerializer.Deserialize<DrawResponse>(json)!;
    }

    public async Task<Card> DrawOneAsync(HttpClient client)
    {
        if (extraCards.Count > 0)
        {
            Card card = extraCards.Pop();
            return card;
        }
        DrawResponse draw = await DrawFromApiAsync(1, client);
        remainingApiCards = draw.remaining;
        return draw.cards![0];
    }

    public void AddCards(Card cards)
    {
        extraCards.Push(cards);
    }

    public string ID => deck_id!;
}

public class DrawResponse
{
    public bool success { get; set; }
    public int remaining { get; set; }
    public Card[]? cards { get; set; }
}
