
public class Hand
{
    public List<Card> Cards { get; private set; } = new();

    public async Task FirstDraw(Deck deck, HttpClient client)
    {
        DrawResponse draw = await deck.DrawCardsAsync(8, client);
        if (draw.cards == null) return;
        foreach (Card card in draw.cards)
        {
            Cards.Add(card);
        }
        deck.remaining = draw.remaining;
    }

    public void DrawToFull(Deck deck)
    {
        while (Cards.Count < 8)
        {
            Cards.Add(deck.DrawOne());
        }
    }

    public List<Card> SelectCards()
    {
        while (true)
        {
            List<Card> selected = new();

            Console.Write("> ");
            string input = Console.ReadLine()!;
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 5 || parts.Length == 0)
            {
                Console.WriteLine("You can only select max 5 cards");
                continue;
            }

            foreach (string part in parts)
            {
                if (int.TryParse(part, out int index))
                {
                    if (index >= 1 && index <= Cards.Count)
                    {
                        selected.Add(Cards[index - 1]);
                    }
                }
            }
            return selected;
        }
    }

    public void RemoveCards(List<Card> toRemove)
    {
        foreach (Card card in toRemove)
            Cards.Remove(card);
    }
}
