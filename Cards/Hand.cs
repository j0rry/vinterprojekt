
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
}
