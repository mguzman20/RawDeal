using RawDeal.Cards;
using RawDealView.Formatters;

namespace RawDeal.Info;

public class PlayInfo : IViewablePlayInfo
{
    private Card _card;
    private int _damage;
    public PlayInfo(IViewableCardInfo cardInfo, string playedAs, Card card, int damage)
    {
        CardInfo = cardInfo;
        PlayedAs = playedAs;
        _card = card;
        _damage = damage;
    }
    public IViewableCardInfo CardInfo { get; }
    public string PlayedAs { get; }
     
    public int Damage() => _damage;
    public Card GetCard() => _card;
}