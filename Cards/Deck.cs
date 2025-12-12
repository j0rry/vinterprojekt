using System.Text.Json;

public class Deck
{
    public string? deck_id { get; set; }
    public int remaining { private get; set; }

    public async Task<DrawResponse> DrawCardsAsync(int amount, HttpClient client)
    {
        if (amount < 0) return new DrawResponse();
        string url = $"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count={amount}";
        string json = await client.GetStringAsync(url);

        return JsonSerializer.Deserialize<DrawResponse>(json)!;
    }

    public Card DrawOne()
    {
        HttpClient client = new();
        DrawResponse draw = DrawCardsAsync(1, client).Result;
        remaining = draw.remaining;
        return draw.cards[0];
    }

    public int Count => remaining;
    public string ID => deck_id!;
}

public class DrawResponse
{
    public bool success { get; set; }
    public int remaining { get; set; }
    public Card[]? cards { get; set; }
}
