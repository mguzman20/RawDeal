using RawDealView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using RawDeal.Utils;
using RawDealView.Options;

namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;
    private Player _playerInTurn;
    private Player _otherPlayer;
    
    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
    }

    public void Play()
    {
        DeckPreparation();
    }

    private void DeckPreparation()
    {
        var pathToFirstDeck = _view.AskUserToSelectDeck(_deckFolder);
        var isFirstDeckValid = ValidateDeck(pathToFirstDeck);
        if (!isFirstDeckValid) return;
        var pathToSecondDeck = _view.AskUserToSelectDeck(_deckFolder);
        var isSecondDeckValid = ValidateDeck(pathToSecondDeck);
        if (!isSecondDeckValid) return;
        GamePreparation(pathToFirstDeck, pathToSecondDeck);
    }
    
    private bool ValidateDeck(string pathToDeck)
    {
        var isValidDeck = new DeckValidation(pathToDeck).CheckDeck();
        if (!isValidDeck) _view.SayThatDeckIsInvalid();
        return isValidDeck;
    }

    private void GamePreparation(string pathToFirstDeck, string pathToSecondDeck)
    {
        var initialPlayer = new Player(File.ReadAllLines(pathToFirstDeck));
        var secondPlayer = new Player(File.ReadAllLines(pathToSecondDeck));
        if (initialPlayer.ReturnSuperstarValue() < secondPlayer.ReturnSuperstarValue())
        {
            (initialPlayer, secondPlayer) = (secondPlayer, initialPlayer);
        }
        initialPlayer.SetSuperstarAbilities(secondPlayer, _view);
        secondPlayer.SetSuperstarAbilities(initialPlayer, _view);
        StartGame(initialPlayer, secondPlayer);
    }

    private void StartGame(Player player1, Player player2)
    {
        _playerInTurn = player1;
        _otherPlayer = player2;
        var turn = new Turn(_playerInTurn, _otherPlayer, _view);
        while (!turn.GameFinished())
        {
            turn.NewTurn();
            if (!turn.GameFinished()) (_playerInTurn, _otherPlayer) = (_otherPlayer, _playerInTurn);
        } 
        EndGame();
    }

    private void EndGame()
    {
        _view.CongratulateWinner(_otherPlayer.ArsenalCount() == 0
            ? _playerInTurn.SuperstarName()
            : _otherPlayer.SuperstarName());
    }
}
