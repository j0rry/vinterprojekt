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
    public Card[]? cards { get; set; }
}

class Card
{
    public string? code { get; set; }
    public string? value { private get; set; } // Kommer nog att behöva använda tryparse för detta senare!
    public string? suit { get; set; }

    private string GetSuitSymbol()
    {
        return suit switch
        {
            "SPADES" => "♠",
            "HEARTS" => "♥",
            "DIAMONDS" => "♦",
            "CLUBS" => "♣",
            _ => "?"
        };
    }

    private string GetDisplayValue()
    {
        if (value == null) return "?";
        return value.ToUpper() switch
        {
            "ACE" => "A",
            "KING" => "K",
            "QUEEN" => "Q",
            "JACK" => "J",
            "JOKER" => "?",
            _ => value
        };
    }

    public int GetValue()
    {
        if (value == null) return 0;
        if (int.TryParse(value, out int num))
        {
            return num;
        }

        return value.ToUpper() switch
        {
            "JACK" => 11,
            "QUEEN" => 12,
            "KING" => 13,
            "ACE" => 14,
            _ => 0
        };
    }

    public string[] GetAscii()
    {
        string symbol = GetSuitSymbol();

        return new[]
        {
            "┌───────┐",
            $"│{GetDisplayValue(),-2}     │",
            $"│   {symbol}   │",
            $"│     {GetDisplayValue(),2}│",
            "└───────┘"
        };
    }

    public override string ToString() => $"{value} of {GetSuitSymbol()} ({suit})";
}
