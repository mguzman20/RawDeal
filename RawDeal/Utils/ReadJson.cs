using System.Text.Json;
using RawDeal.Info;

namespace RawDeal.Utils;

internal static class ReadJson
{
    public static List<SuperstarInfo> ReadSuperstarFile()
    {
        var myJson = File.ReadAllText(Path.Combine("data", "superstar2.json"));
        var superstarInfo = JsonSerializer.Deserialize<List<SuperstarInfo>>(myJson);
        return superstarInfo;
    }
    
    public static List<CardsInfo?> ReadCardsFile()
    {
        var myJson = File.ReadAllText(Path.Combine("data", "cards.json"));
        var cardsInfo = JsonSerializer.Deserialize<List<CardsInfo>>(myJson);
        return cardsInfo;
    }
}