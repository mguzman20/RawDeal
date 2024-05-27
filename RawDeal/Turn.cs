using RawDeal.Cards;
using RawDeal.Info;
using RawDeal.Utils;
using RawDealView;
using RawDealView.Formatters;
using RawDealView.Options;

namespace RawDeal;

public class Turn
{
    private Player _playerInTurn;
    private Player _otherPlayer;
    private View _view;
    private bool _turnFinished;
    private bool _gameFinished;
    private PlayInfo _lastPlay;
    
    public Turn(Player playerInTurn, Player otherPlayer, View view)
    {
        _playerInTurn = playerInTurn;
        _otherPlayer = otherPlayer;
        _view = view;
        _turnFinished = false;
        _gameFinished = false;
    }

    public void NewTurn()
    {
        _turnFinished = false;
        _view.SayThatATurnBegins(_playerInTurn.SuperstarName());
        _playerInTurn.NewTurn();
        _playerInTurn.UseSuperstarAbilityAtStartOfTurn();
        _playerInTurn.DrawCardAtStartOfTurn();
        StartTurn();
    }

    private void StartTurn()
    {
        while (!_turnFinished)
        {
            _view.ShowGameInfo(_playerInTurn.ReturnPlayerInfo(), _otherPlayer.ReturnPlayerInfo());
            ManageOptionToChose();
        }
        if (_playerInTurn.ArsenalCount() == 0 || _otherPlayer.ArsenalCount() == 0) _gameFinished = true;
    }
    
    private void EndTurn()
    {
        _turnFinished = true;
        _playerInTurn.AddExtraDamage(0);
        _playerInTurn.AddExtraFortitude(0);
        (_playerInTurn, _otherPlayer) = (_otherPlayer, _playerInTurn);
    }
    
    public bool GameFinished() => _gameFinished;
    
    private void ManageOptionToChose()
    {
        NextPlay optionChose;
        if (_playerInTurn.PlayerHasUsedHisAbility())
            optionChose = _view.AskUserWhatToDoWhenHeCannotUseHisAbility();
        else
            optionChose = _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        switch (optionChose)
        {
            case NextPlay.ShowCards:
                ShowCards();
                break;
            case NextPlay.PlayCard:
                PlayCard();
                break;
            case NextPlay.UseAbility:
                _playerInTurn.UseSuperstarAbilityInTurn();
                break;
            case NextPlay.EndTurn:
                _turnFinished = true;
                EndTurn();
                break;
            case NextPlay.GiveUp:
                _gameFinished = true;
                _turnFinished = true;
                break;
        }
    }

    private void ShowCards()
    {
        var optionChose = _view.AskUserWhatSetOfCardsHeWantsToSee();
        switch (optionChose)
        {
            case CardSet.Hand:
                _view.ShowCards(_playerInTurn.ReturnHand());
                break;
            case CardSet.RingArea:
                _view.ShowCards(_playerInTurn.ReturnRing());
                break;
            case CardSet.RingsidePile:
                _view.ShowCards(_playerInTurn.ReturnRingside());
                break;
            case CardSet.OpponentsRingArea:
                _view.ShowCards(_otherPlayer.ReturnRing());
                break;
            case CardSet.OpponentsRingsidePile:
                _view.ShowCards(_otherPlayer.ReturnRingside());
                break;
        }
    }
    
    private void PlayCard()
    {
        var plays = _playerInTurn.CardsPlayable();
        var option = SelectPlay(plays);
        if (option == -1) return;
        _view.SayThatPlayerIsTryingToPlayThisCard(_playerInTurn.SuperstarName(), 
            Formatter.PlayToString(plays[option]));
        if (CheckReversalFromHand(plays[option])) UseReversalFromHand(plays[option]);
        else UseCard(plays[option]);
        _lastPlay = plays[option];
    }

    private int SelectPlay(List<PlayInfo> plays)
    {
        var formattedPlays = new List<string>();
        foreach (var play in plays) formattedPlays.Add(Formatter.PlayToString(play));
        return _view.AskUserToSelectAPlay(formattedPlays);
    }
    
    private void UseCard(PlayInfo play)
    {
        switch (play.PlayedAs)
        {
            case "ACTION":
                _view.SayThatPlayerSuccessfullyPlayedACard();
                if (play.CardInfo.Title != "Jockeying for Position")
                {
                    _playerInTurn.DiscardCardFromHandToRingside(play.GetCard()); 
                }
                UseEffects(play);
                break;
            case "MANEUVER":
                UseManeuver(play);
                _playerInTurn.AddExtraDamage(0);
                _playerInTurn.AddExtraFortitude(0);
                break;
        }
    }

    private void UseManeuver(PlayInfo play)
    {
        var damage = play.Damage();
        if (_otherPlayer.MankindAbility()) damage -= 1;
        _playerInTurn.DiscardCardFromHandToRing(play.GetCard());
        _view.SayThatPlayerSuccessfullyPlayedACard();
        UseEffects(play);
        if (damage == 0) return;
        _view.SayThatSuperstarWillTakeSomeDamage(_otherPlayer.SuperstarName(), damage);
        MakeDamage(play, damage);
    }

    private void MakeDamage(PlayInfo play, int damage)
    {
        for (var i = 0; i < damage; i++)
        {
            var discardedCard = _otherPlayer.Take1Damage();
            if (discardedCard == null)
            {
                EndTurn();
                break;
            }
            _view.ShowCardOverturnByTakingDamage(Format.Card(discardedCard), i + 1, damage);
            if (CheckForReversal(play, discardedCard))
            {
                UseReversalFromDeck(play, i,damage);
                break;
            }
        }
    }

    private void UseEffects(PlayInfo play)
    {
        if (play.CardInfo.Types.Contains("Action") && play.CardInfo.Types.Contains("Maneuver"))
        {
            if (play.PlayedAs == "ACTION")
            {
                play.GetCard().UseEffects(_playerInTurn, _otherPlayer, _view);
            }
        }
        else if (play.CardInfo.Title == "Jockeying for Position" && play.PlayedAs == "ACTION")
        {
            play.GetCard().UseEffects(_playerInTurn, _otherPlayer, _view);
            _playerInTurn.DiscardCardFromHandToRing(play.GetCard());
        }
        else
        {
            play.GetCard().UseEffects(_playerInTurn, _otherPlayer, _view);
        }
    }
    private bool CheckReversalFromHand(PlayInfo play)
    {
        var hand = _otherPlayer.Hand();
        foreach (var card in hand)
        {
            if (CheckForReversal(play, card)) return true;
        }
        return false;
    }
         
    private bool CheckForReversal(PlayInfo play, Card cardReversing)
    {
        if (Reversal.IsReversalToACard(play, cardReversing) && cardReversing.CardInfo().Types.Contains("Reversal"))
        {
            var fortitude = int.Parse(cardReversing.CardInfo().Fortitude);
            fortitude += _playerInTurn.ExtraFortitude();
            if (_otherPlayer.ReturnFortitude() >= fortitude) return true;
        }
        return false;
    }
    
    private void UseReversalFromDeck(PlayInfo play, int currentDamage, int totalDamage)
    {
        _view.SayThatCardWasReversedByDeck(_otherPlayer.SuperstarName());
        if (int.Parse(play.GetCard().CardInfo().StunValue) > 0 && currentDamage < totalDamage - 1)
        {
            UseStunValue(play);
        }
        EndTurn();
    }
    
    private void UseReversalFromHand(PlayInfo play)
    {
        var reversals = _otherPlayer.FindReversals(play);
        var option = _view.AskUserToSelectAReversal(_otherPlayer.SuperstarName(), 
            Format.Reversals(reversals));
        if (option == -1) UseCard(play);
        else
        {
            var reversal = reversals[option];
            _view.SayThatPlayerReversedTheCard(_otherPlayer.SuperstarName(), Format.Reversal(reversal));
            var damage = play.Damage();
            if (_otherPlayer.MankindAbility() && damage != 1) damage -= 1;
            Reversal.AddEffect(reversal, damage);
            _playerInTurn.DiscardCardFromHandToRingside(play.GetCard());
            reversal.UseEffects(_otherPlayer, _playerInTurn, _view);
            _otherPlayer.DiscardCardFromHandToRing(reversal);
            EndTurn();
        }
    }

    private void UseStunValue(PlayInfo play)
    {
        var quantity = _view.AskHowManyCardsToDrawBecauseOfStunValue(_playerInTurn.SuperstarName(), 
            int.Parse(play.GetCard().CardInfo().StunValue));
        if (quantity == 0) return;
        for (var j = 0; j < quantity; j++) _playerInTurn.DrawCard(); 
        _view.SayThatPlayerDrawCards(_playerInTurn.SuperstarName(), quantity);
    }
    
}