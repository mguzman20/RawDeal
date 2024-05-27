using RawDeal.Info;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards;

public class Card
{
    private List<CardEffects> _effects;
    private string _name;
    private CardsInfo? _cardInfo;
    
    public Card(string name)
    {
        _name = name;
        _cardInfo = ReadJson.ReadCardsFile().Find(x => x.Title == name);
        _effects = new List<CardEffects>();
        CardOptions.GetEffectsList(this, name);
    }
    public CardsInfo CardInfo() => _cardInfo;
    
    public string Title() => _name;
    
    public bool IsReversal() => _cardInfo.Types.Contains("Reversal");
    
    public void AddEffect(CardEffects effect)
    {
        if (!_effects.Contains(effect)) _effects.Add(effect);
    }
    
    public void UseEffects(Player player,Player opponent, View view)
    {
        foreach (var effect in _effects)
        {
            effect.UseEffect(player, opponent, view);
        }
    }
}