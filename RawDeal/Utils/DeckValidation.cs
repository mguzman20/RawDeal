using RawDeal.Info;

namespace RawDeal.Utils;

public class DeckValidation
{
    private string _deckFolder;
    private readonly List<CardsInfo?> _cardsInfo;
    private readonly List<SuperstarInfo> _superstarsInfo;
    private readonly string[] _linesOfTheDeck;

    public DeckValidation(string deckFolder)
    {
        _deckFolder = deckFolder;
        _cardsInfo = ReadJson.ReadCardsFile();
        _superstarsInfo = ReadJson.ReadSuperstarFile();
        _linesOfTheDeck = File.ReadAllLines(_deckFolder);
    }

    public bool CheckDeck()
    {
        return CheckDeckLength() && CheckUniqueCards() && CheckSetUpCards() && CheckHeelAndFaceCards() && CheckSuperstarCard();
    }

    private bool CheckDeckLength()
    {
        return _linesOfTheDeck.Length == 61;
    }

    private bool CheckUniqueCards()
    {
        var repeatedLines = _linesOfTheDeck.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key);
        foreach (var line in repeatedLines)
        {
            var card = _cardsInfo.FirstOrDefault(x => x.Title == line);
            if (card.Types.Concat(card.Subtypes).Any(type => type == "Unique")) return false;
        }
        return true;
    }
    
    private bool CheckSetUpCards()
    {
        var repeatedFourTimes = _linesOfTheDeck.GroupBy(x => x).Where(g => g.Count() > 3).Select(y => y.Key);
        if (repeatedFourTimes.Count() != 0)
        {
            foreach (var line in repeatedFourTimes)
            {
                var card = _cardsInfo.Find(x => x.Title == line);
                var cardTypes = card.Types.Concat(card.Subtypes);
                if (!cardTypes.Contains("SetUp")) return false;
            }
        }
        return true;
    }
    
    private bool CheckHeelAndFaceCards()
    {
        var thereAreHeelCards = false;
        var thereAreFaceCards = false;
        foreach (var line in _linesOfTheDeck.Skip(1))
        {
            var card = _cardsInfo.FirstOrDefault(x => x.Title == line);
            foreach (var type in card.Types.Concat(card.Subtypes))
            {
                switch (type)
                {
                    case "Heel":
                        thereAreHeelCards = true;
                        break;
                    case "Face":
                        thereAreFaceCards = true;
                        break;
                }
            }
        }
        return !thereAreHeelCards || !thereAreFaceCards;
    }
    
    private bool CheckSuperstarCard()
    {
        var superstarInDeck = _superstarsInfo.Find(x => _linesOfTheDeck[0].Contains(x.Name));

        for (var i = 1; i < _linesOfTheDeck.Length; i++)
        {
            var card = _cardsInfo.Find(x => x.Title == _linesOfTheDeck[i]);
            foreach (var superstar in _superstarsInfo)
            {
                var types = card.Subtypes;
                if (types.Contains(superstar.Logo) && superstar.Name != superstarInDeck.Name) return false;
            }
        }
        return true;
    }
}    