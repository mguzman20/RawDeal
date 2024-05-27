using RawDeal.Utils;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.Cards;

public abstract class CardEffects
{
    public abstract void UseEffect(Player player, Player opponent, View view);
}


public class PlayerMayDrawCard : CardEffects
{
    private readonly int _quantity;
    
    public PlayerMayDrawCard(int quantity)
    {
        _quantity = quantity;
    }
    public override void UseEffect(Player player, Player opponent, View view)
    {
        var quantity = view.AskHowManyCardsToDrawBecauseOfACardEffect(player.SuperstarName(), _quantity);
        for (var i = 0; i < quantity; i++) player.DrawCard();
        view.SayThatPlayerDrawCards(player.SuperstarName(), quantity);
    }
}

public class DiscardCard : CardEffects
{
    private Card _card;
    
    public DiscardCard(Card card)
    {
        _card = card;
    }

    public override void UseEffect(Player player, Player opponent, View view)
    {
        view.SayThatPlayerMustDiscardThisCard(player.SuperstarName(), _card.Title());
    }
}

public class OpponentDamage : CardEffects
{
    private int _damage;
    public OpponentDamage(int damage)
    {
        _damage = damage;
    }

    public override void UseEffect(Player player,Player opponent, View view)
    {
        if (opponent.MankindAbility()) _damage -= 1;
        if (_damage < 1) return;
        view.SayThatSuperstarWillTakeSomeDamage(opponent.SuperstarName(), _damage);
        for (var i = 0; i < _damage; i++)
        {
            var discardedCard = opponent.Take1Damage();
            if (discardedCard == null) break;
            view.ShowCardOverturnByTakingDamage(Format.Card(discardedCard), i + 1, _damage);
        }
    }
}

public class JockeyingForPosition : CardEffects
{
    private Card _card;
    
    public JockeyingForPosition(Card card) => _card = card;
    public override void UseEffect(Player player, Player opponent, View view)
    {
        var effect = view.AskUserToSelectAnEffectForJockeyForPosition(player.SuperstarName());
        switch (effect)
        {
            case SelectedEffect.NextGrappleIsPlus4D:
                player.AddExtraDamage(4);
                break;
            case SelectedEffect.NextGrapplesReversalIsPlus8F:
                player.AddExtraFortitude(8);
                break;
        }
    }
}

public class OpponentDiscardCard : CardEffects
{
    private readonly int _quantity;
    public OpponentDiscardCard(int quantity)
    {
        _quantity = quantity;
    }

    public override void UseEffect(Player player, Player opponent, View view)
    {
        for (var i = _quantity; i > 0; i--)
        {
            if (opponent.HandCount() <= 0) return;
            var option = view.AskPlayerToSelectACardToDiscard(opponent.ReturnHand(), 
                opponent.SuperstarName(),
                opponent.SuperstarName(), i);
            opponent.DiscardCardFromHandToRingside(option);
        }
    }
}

public class PlayerDiscardCard : CardEffects
{
    private readonly int _quantity;
    public PlayerDiscardCard(int quantity) =>_quantity = quantity;
    

    public override void UseEffect(Player player, Player opponent, View view)
    {
        for (var i = _quantity; i > 0; i--)
        {
            if (player.HandCount() <= 0) return;
            var option = view.AskPlayerToSelectACardToDiscard(player.ReturnHand(),
                player.SuperstarName(),
                player.SuperstarName(),
                i);
            player.DiscardCardFromHandToRingside(option);
        }
    }
}

public class PlayerDiscardCardFromOpponent : CardEffects
{
    private readonly int _quantity;
    public PlayerDiscardCardFromOpponent(int quantity)
    {
        _quantity = quantity;
    }

    public override void UseEffect(Player player, Player opponent, View view)
    {
        for (var i = _quantity; i > 0; i--)
        {
            if (opponent.HandCount() <= 0) return;
            var option = view.AskPlayerToSelectACardToDiscard(opponent.ReturnHand(),
                opponent.SuperstarName(),
                player.SuperstarName(),
                i);
            opponent.DiscardCardFromHandToRingside(option);
        }
    }
}

public class PLayerDamageHimself : CardEffects
{
    public override void UseEffect(Player player, Player opponent, View view)
    {
        view.SayThatPlayerDamagedHimself(player.SuperstarName(), 1);
        var discardedCard = player.Take1Damage();
        view.SayThatSuperstarWillTakeSomeDamage(player.SuperstarName(), 1);
        if (player.ArsenalCount() == 0 || discardedCard == null)
        {
            view.SayThatPlayerLostDueToSelfDamage(player.SuperstarName());
            return;
        }
        view.ShowCardOverturnByTakingDamage(Format.Card(discardedCard), 1, 1);
    }
}

public class OpponentDrawCard : CardEffects
{
    private readonly int _quantity;
    public OpponentDrawCard(int quantity)
    {
        _quantity = quantity;
    }

    public override void UseEffect(Player player, Player opponent, View view)
    {
        for (var i = 0; i < _quantity; i++) opponent.DrawCard();
        view.SayThatPlayerDrawCards(opponent.SuperstarName(), _quantity);
    }
}

public class RecoverDamage : CardEffects
{
    private readonly int _quantity;
    public RecoverDamage(int quantity) => _quantity = quantity;

    public override void UseEffect(Player player, Player opponent, View view)
    {
        for (var i = 0; i < _quantity; i++)
        {
            var option =
                view.AskPlayerToSelectCardsToRecover(player.SuperstarName(), _quantity, player.ReturnRingside());
            player.RecoverCardFromRingsideToArsenal(option);
        }
    }
}

public class ChoseBetweenDrawOrDiscard : CardEffects
{
    private readonly int _quantity;

    public ChoseBetweenDrawOrDiscard(int quantity) => _quantity = quantity;
    
    public override void UseEffect(Player player, Player opponent, View view)
    {
        var option = 
            view.AskUserToChooseBetweenDrawingOrForcingOpponentToDiscardCards(player.SuperstarName());
        switch (option)
        {
            case SelectedEffect.DrawCards:
                new PlayerMayDrawCard(_quantity).UseEffect(player, opponent, view);
                break;
            case SelectedEffect.ForceOpponentToDiscard:
                new OpponentDiscardCard(_quantity).UseEffect(player, opponent, view);
                break;
        }
    }
}

public class PlayerDrawCard : CardEffects
{
    private readonly int _quantity;
    public PlayerDrawCard(int quantity) => _quantity = quantity;

    public override void UseEffect(Player player, Player opponent, View view)
    {
        for (var i = 0; i < _quantity; i++) player.DrawCard();
        view.SayThatPlayerDrawCards(player.SuperstarName(), _quantity);
    }
}