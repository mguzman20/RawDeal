using RawDealView;
namespace RawDeal.Superstars;

static class SuperstarOptions
{
    public static SuperstarAbilities GetSuperstar(string superstar, Player player, Player opponent, View view, string superstarAbility)
    {
        if (superstar.Contains("HHH")) return new HHH("HHH", player, opponent, view, superstarAbility);
        if (superstar.Contains("KANE")) return new Kane("KANE", player, opponent, view, superstarAbility);
        if (superstar.Contains("THE ROCK")) return new TheRock("THE ROCK", player, opponent, view, superstarAbility);
        if (superstar.Contains("UNDERTAKER")) return new Undertaker("THE UNDERTAKER", player, opponent, view, superstarAbility);
        if (superstar.Contains("CHRIS JERICHO")) return new Jericho("CHRIS JERICHO", player, opponent, view, superstarAbility);
        if (superstar.Contains("MANKIND")) return new Mankind("MANKIND", player, opponent, view, superstarAbility);
        if (superstar.Contains("STONE COLD")) return new StoneCold("STONE COLD STEVE AUSTIN", player, opponent, view, superstarAbility);
        throw new ArgumentException("One of the options is invalid.");
    }
}
