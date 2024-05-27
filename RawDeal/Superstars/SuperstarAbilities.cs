using RawDealView;
using RawDealView.Formatters;

namespace RawDeal.Superstars;

public abstract class SuperstarAbilities
{
    protected readonly string SuperstarName;
    private readonly string _superstarAbility;
    protected bool AbilityUsed;
    protected readonly Player Player;
    protected readonly Player Opponent;
    protected readonly View View;
    protected SuperstarAbilities(string superstarName, Player player, Player opponent, View view, string superstarAbility)
    {
        SuperstarName = superstarName;
        AbilityUsed = false;
        Player = player;
        Opponent = opponent;
        View = view;
        _superstarAbility = superstarAbility;
    }
    public abstract bool HasUsedAbility();
    public abstract void NewTurn();
    public abstract void UseAbilityInTurn();
    public abstract void UseAbilityAtStartOfTurn();
    protected void StartOutput() => View.SayThatPlayerIsGoingToUseHisAbility(SuperstarName, _superstarAbility);
}


public class HHH : SuperstarAbilities
{
    public HHH(string superstarName, Player player, Player opponent, View view, string superstarAbility) : base(superstarName, player, opponent, view, superstarAbility) {}
    public override bool HasUsedAbility() => true;
    public override void NewTurn(){}
    public override void UseAbilityInTurn(){}
    public override void UseAbilityAtStartOfTurn(){}
}


public class Kane : SuperstarAbilities
{
    public Kane(string superstarName, Player player, Player opponent, View view, string superstarAbility) : base(superstarName, player, opponent, view, superstarAbility) {}
    public override bool HasUsedAbility() => AbilityUsed;
    public override void NewTurn() => AbilityUsed = false;
    public override void UseAbilityInTurn() {}
    public override void UseAbilityAtStartOfTurn()
    {
        if (AbilityUsed) return;
        StartOutput();
        const int damageToBeReceived = 1;
        var discardedCard = Opponent.DiscardCardFromArsenalToRingside();
        View.SayThatSuperstarWillTakeSomeDamage(Opponent.SuperstarName(), damageToBeReceived);
        View.ShowCardOverturnByTakingDamage(Formatter.CardToString(discardedCard.CardInfo()), damageToBeReceived, damageToBeReceived);
        AbilityUsed = true;
    }
}

public class TheRock : SuperstarAbilities
{
    public TheRock(string superstarName, Player player, Player opponent, View view, string superstarAbility) : base(superstarName, player, opponent, view, superstarAbility) {}
    public override bool HasUsedAbility() => AbilityUsed;
    public override void NewTurn() => AbilityUsed = false;
    public override void UseAbilityInTurn() {}

    public override void UseAbilityAtStartOfTurn()
    {
        AbilityUsed = true;
        if (Player.ReturnRingside().Count == 0) return;
        var playerWantToUseHisAbility = View.DoesPlayerWantToUseHisAbility(SuperstarName);
        if (!playerWantToUseHisAbility) return;
        var optionChose = View.AskPlayerToSelectCardsToRecover(SuperstarName, 1, Player.ReturnRingside());
        Player.RecoverCardFromRingsideToArsenal(optionChose);
    }
}


public class Undertaker : SuperstarAbilities
{
    public Undertaker(string superstarName, Player player, Player opponent, View view, string superstarAbility) : base(superstarName, player, opponent, view, superstarAbility) {}
    public override bool HasUsedAbility() => Player.HandCount() < 2 || AbilityUsed;
    public override void NewTurn() => AbilityUsed = false;
    public override void UseAbilityInTurn()
    {
        if (AbilityUsed || Player.HandCount() < 2) return;
        StartOutput();
        var firstCardToDiscard = View.AskPlayerToSelectACardToDiscard(Player.ReturnHand(), SuperstarName, SuperstarName, 2);
        Player.DiscardCardFromHandToRingside(firstCardToDiscard);
        var secondCardToDiscard = View.AskPlayerToSelectACardToDiscard(Player.ReturnHand(), SuperstarName, SuperstarName, 1);
        Player.DiscardCardFromHandToRingside(secondCardToDiscard);
        var cardToPutInHisHand = View.AskPlayerToSelectCardsToPutInHisHand(SuperstarName, 1, Player.ReturnRingside());
        Player.RecoverCardFromRingsideToHand(cardToPutInHisHand);
        AbilityUsed = true;
    }
    public override void UseAbilityAtStartOfTurn() {}
}


public class Jericho : SuperstarAbilities
{
    public Jericho(string superstarName, Player player, Player opponent, View view, string superstarAbility) : base(
        superstarName, player, opponent, view, superstarAbility) {}
    public override bool HasUsedAbility() => Player.ReturnHand().Count < 1 || AbilityUsed;
    public override void NewTurn() => AbilityUsed = false;
    public override void UseAbilityInTurn()
    {
        if (AbilityUsed || Player.ReturnHand().Count < 1) return;
        StartOutput();
        var cardToDiscard = View.AskPlayerToSelectACardToDiscard(Player.ReturnHand(), SuperstarName, SuperstarName, 1);
        Player.DiscardCardFromHandToRingside(cardToDiscard);
        var cardToDiscardFromOpponent = View.AskPlayerToSelectACardToDiscard(Opponent.ReturnHand(),
            Opponent.SuperstarName(), Opponent.SuperstarName(), 1);
        Opponent.DiscardCardFromHandToRingside(cardToDiscardFromOpponent);
        AbilityUsed = true;
    }
    public override void UseAbilityAtStartOfTurn() {}
}


public class Mankind : SuperstarAbilities
{
    public Mankind(string superstarName, Player player, Player opponent, View view, string superstarAbility) : base(
        superstarName, player, opponent, view, superstarAbility)
    {
        Player.HasMankindAbility();
        AbilityUsed = true;
    }
    public override bool HasUsedAbility() => AbilityUsed;
    public override void NewTurn(){}
    public override void UseAbilityInTurn(){}
    public override void UseAbilityAtStartOfTurn() {}
}


public class StoneCold : SuperstarAbilities
{
    public StoneCold(string superstarName, Player player, Player opponent, View view, string superstarAbility) : base(superstarName, player, opponent, view, superstarAbility) {}
    public override bool HasUsedAbility() => Player.ArsenalCount() < 1 || AbilityUsed;
    public override void NewTurn() => AbilityUsed = false;
    public override void UseAbilityInTurn()
    {
        if (AbilityUsed || Player.ArsenalCount() < 1) return;
        StartOutput();
        View.SayThatPlayerDrawCards(SuperstarName, 1);
        Player.DrawCard();
        var cardToDiscard = View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(SuperstarName, Player.ReturnHand());
        Player.DiscardCardFromHandToArsenal(cardToDiscard);
        AbilityUsed = true;
    }
    public override void UseAbilityAtStartOfTurn() {}
}
