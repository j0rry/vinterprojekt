

public static class UI
{
    public static void ShowHand(Card[] cards)
    {
        List<string[]> asciiCards = new List<string[]>();

        foreach (Card c in cards)
            asciiCards.Add(c.GetAscii());

        for (int line = 0; line < asciiCards[0].Length; line++)
        {
            for (int i = 0; i < asciiCards.Count; i++)
            {
                Console.Write(asciiCards[i][line] + "  ");
            }
            Console.WriteLine();
        }
    }
}
