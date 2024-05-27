using RawDeal.Cards;
using RawDeal.Info;
using RawDealView.Formatters;

namespace RawDeal.Utils;

public static class Format
{
    public static List<string> Cards(List<Card> cards)
    {
        var formattedCards = new List<string>();
        foreach (var card in cards)
        {
            var cardInfo = card.CardInfo();
            var formattedCard = Formatter.CardToString(cardInfo);
            formattedCards.Add(formattedCard);
        }
        return formattedCards;
    }
    
    public static string Card(Card card)
    {
        var cardInfo = card.CardInfo();
        var formattedCard = Formatter.CardToString(cardInfo);
        return formattedCard;
    }
    
    public static List<string> Reversals(List<Card> cards)
    {
        var formattedCards = new List<string>();
        foreach (var card in cards)
        {
            var cardInfo = card.CardInfo();
            var playInfo = new PlayInfo(cardInfo, "REVERSAL", card, 0);
            var formattedCard = Formatter.PlayToString(playInfo);
            formattedCards.Add(formattedCard);
        }
        return formattedCards;
    }
    
    public static string Reversal(Card card)
    {
        var cardInfo = card.CardInfo();
        var playInfo = new PlayInfo(cardInfo, "REVERSAL", card, 0);
        var formattedCard = Formatter.PlayToString(playInfo);
        return formattedCard;
    }
}