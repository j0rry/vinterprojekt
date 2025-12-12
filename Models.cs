using System.Text.Json;

class Deck
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

    public int Count => remaining;
}

class DrawResponse
{
    public bool success { get; set; }
    public int remaining { get; set; }
    public Card[] cards { get; set; }
}

class Card
{
    public string? code { get; set; }
    public string? value { get; set; } // Kommer nog att behöva använda tryparse för detta senare!
    public string? suit { get; set; }
}
