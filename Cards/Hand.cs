
public class Hand
{
    public List<Card> Cards { get; private set; } = new();
    public int maxHandSize = 8;

    public async Task FirstDraw(Deck deck, HttpClient client)
    {
        Cards.Clear();

        for (int i = 0; i < maxHandSize; i++)
        {
            Cards.Add(await deck.DrawOneAsync(client));
        }
    }

    public async Task DrawToFullAsync(Deck deck, HttpClient client)
    {
        while (Cards.Count < maxHandSize)
        {
            Cards.Add(await deck.DrawOneAsync(client));
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
