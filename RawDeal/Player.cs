using RawDeal.Cards;
using RawDeal.Info;
using RawDeal.Superstars;
using RawDeal.Utils;
using RawDealView;
using RawDealView.Options;

namespace RawDeal;

public class Player
{
    private string _superstarName;
    private SuperstarInfo? _superstarInfo;
    private SuperstarAbilities _superstar;
    private List<Card> _hand;
    private List<Card> _arsenal;
    private List<Card> _ringside;
    private List<Card> _ring;
    private bool _hasMankindAbility;
    private int _extraDamage;
    private int _extraFortitude;


    public Player(string[] deck)
    {
        _hand = new List<Card>();
        _arsenal = new List<Card>();
        _ringside = new List<Card>();
        _ring = new List<Card>();

        _superstarName = deck[0];
        _superstarInfo = ReadJson.ReadSuperstarFile().Find(x => deck[0].Contains(x.Name));

        _hasMankindAbility = false;
        _extraDamage = 0;
        _extraFortitude = 0;

        for (var i = 1; i < (61 - _superstarInfo.HandSize); i++) _arsenal.Add(new Card(deck[i]));
        for (var i = (61 - _superstarInfo.HandSize); i < 61; i++) _hand.Add(new Card(deck[i]));
        _hand.Reverse();
    }
    
    public void SetSuperstarAbilities(Player opponent, View view)
    {
        _superstar =
            SuperstarOptions.GetSuperstar(_superstarName, this, opponent, 
                view, _superstarInfo.SuperstarAbility);
    }
    
    public bool PlayerHasUsedHisAbility()
    {
        return _superstar.HasUsedAbility();
    }
    
    public void UseSuperstarAbilityAtStartOfTurn()
    {
        _superstar.UseAbilityAtStartOfTurn();
    }

    public void UseSuperstarAbilityInTurn()
    {
        _superstar.UseAbilityInTurn();
    }

    public void NewTurn()
    {
        _superstar.NewTurn();
    }
    public string SuperstarName() => _superstarInfo.Name;
    public List<string> ReturnHand() => Format.Cards(_hand);
    public List<string> ReturnRing() => Format.Cards(_ring);
    public List<string> ReturnRingside() => Format.Cards(_ringside);
    public int ReturnSuperstarValue() => _superstarInfo.SuperstarValue;
    public PlayerInfo ReturnPlayerInfo()
    {
        var playerInfo = new PlayerInfo(_superstarInfo.Name, ReturnFortitude(), 
            _hand.Count, _arsenal.Count);
        return playerInfo;
    }

    public void DrawCard()
    {
        var card = _arsenal.Last();
        _arsenal.RemoveAt(_arsenal.Count - 1);
        _hand.Add(card);
    }
    
    public void DrawCardAtStartOfTurn()
    {
        var card = _arsenal.Last();
        _arsenal.RemoveAt(_arsenal.Count - 1);
        _hand.Add(card);
        if (!_hasMankindAbility || _arsenal.Count <= 0) return;
        var card2 = _arsenal.Last();
        _arsenal.RemoveAt(_arsenal.Count - 1);
        _hand.Add(card2);
    }

    public int ReturnFortitude()
    {
        var fortitude = 0;
        foreach (var card in _ring)
        {
            var cardInfo = card.CardInfo();
            if (cardInfo.Damage == "#") continue;
            fortitude += int.Parse(cardInfo.Damage);
        }
        return fortitude;
    }
    
    public List<PlayInfo> CardsPlayable()
    {
        var fortitude = ReturnFortitude();
        var playableCards = new List<PlayInfo>();
        for (var i = 0; i < _hand.Count; i++)
        {
            var cardInfo = _hand[i].CardInfo();
            if (int.Parse(cardInfo.Fortitude) > fortitude) continue;
            foreach (var type in cardInfo.Types)
            {
                if (type is not ("Maneuver" or "Action")) continue;
                var damage = int.Parse(cardInfo.Damage);
                if (cardInfo.Subtypes.Contains("Grapple"))
                {
                    damage += _extraDamage;
                    playableCards.Add(new PlayInfo(cardInfo ,type.ToUpper(), _hand[i], damage));
                }
                else playableCards.Add(new PlayInfo(cardInfo ,type.ToUpper(), _hand[i], damage));
            }
        }
        return playableCards;
    }
    public Card? Take1Damage()
    {
        if (ArsenalCount() <= 0) return null;
        var card = _arsenal.Last();
        _arsenal.RemoveAt(_arsenal.Count - 1);
        _ringside.Add(card);
        return card;
    }

    public List<Card> FindReversals(PlayInfo play)
    {
        var reversals = new List<Card>();
        foreach (var card in _hand)
        {
            if (!card.CardInfo().Types.Contains("Reversal")) continue;
            if (ReturnFortitude() < int.Parse(card.CardInfo().Fortitude)) continue;
            if (!Reversal.IsReversalToACard(play, card)) continue;
            reversals.Add(card);
        }
        return reversals; 
    }

    public int ArsenalCount() => _arsenal.Count;
    public int HandCount() => _hand.Count;
    public void RecoverCardFromRingsideToArsenal(int option)
    {
        var card = _ringside[option];
        _ringside.RemoveAt(option);
        _arsenal.Insert(0, card);
    }
    
    public void RecoverCardFromRingsideToHand(int option)
    {
        var card = _ringside[option];
        _ringside.RemoveAt(option);
        _hand.Add(card);
    }
    
    public void DiscardCardFromHandToRingside(int option)
    {
        var card = _hand[option];
        _hand.RemoveAt(option);
        _ringside.Add(card);
    }
    
    public void DiscardCardFromHandToRingside(Card card)
    {
        _hand.Remove(card);
        _ringside.Add(card);
    }
    
    public void DiscardCardFromHandToArsenal(int option)
    {
        var card = _hand[option];
        _hand.RemoveAt(option);
        _arsenal.Insert(0, card);
    }
    
    public Card DiscardCardFromArsenalToRingside()
    {
        var card = _arsenal.Last();
        _arsenal.Remove(card);
        _ringside.Add(card);
        return card;
    }
    
    public void DiscardCardFromHandToRing(Card card)
    {
        _hand.Remove(card);
        _ring.Add(card);
    }
    public void HasMankindAbility() => _hasMankindAbility = true;
    public bool MankindAbility() => _hasMankindAbility;
    
    public List<Card> Hand() => _hand;

    public void AddExtraDamage(int quantity) => _extraDamage = quantity;

    public void AddExtraFortitude(int quantity) => _extraFortitude = quantity;

    public int ExtraDamage() => _extraDamage;
    public int ExtraFortitude() => _extraFortitude;
}