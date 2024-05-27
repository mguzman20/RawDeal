using RawDeal.Info;

namespace RawDeal.Cards;

public static class Reversal
{
    public static bool IsReversalToACard(PlayInfo play, Card cardReversing)
    {
        var cardToReverse = play.GetCard();
        var playAs = play.PlayedAs;
        var damage = play.Damage();
        if (ContainsSubtype("Grapple",
                cardToReverse) &&
            ContainsSubtype("ReversalGrappleSpecial",
                cardReversing) &&
            damage <= 7 &&
            playAs == "MANEUVER")
            return true;
        if (ContainsSubtype("Strike",
                cardToReverse) &&
            ContainsSubtype("ReversalStrikeSpecial",
                cardReversing) &&
            damage <= 7 &&
            playAs == "MANEUVER")
            return true;
        if (ContainsSubtype("ReversalSpecial", cardReversing)) return CheckReversalSpecial(play, cardReversing);
        switch (playAs)
        {
            case "ACTION" when ContainsSubtype("ReversalAction", cardReversing) &&
                               cardToReverse.CardInfo().Types.Contains("Action"):
            case "MANEUVER" when ContainsSubtype("ReversalGrapple", cardReversing) &&
                                 ContainsSubtype("Grapple", cardToReverse):
            case "MANEUVER" when ContainsSubtype("ReversalStrike", cardReversing) &&
                                 ContainsSubtype("Strike", cardToReverse):
            case "MANEUVER" when ContainsSubtype("ReversalSubmission", cardReversing) &&
                                 ContainsSubtype("Submission", cardToReverse):
                return true;
            default:
                return false;
        }
    }

    private static bool CheckReversalSpecial(PlayInfo play, Card cardReversing)
    {
        switch (cardReversing.Title())
        {
            case "Elbow to the Face" when play.Damage() <= 7 && play.PlayedAs == "MANEUVER":
            case "Manager Interferes" when play.PlayedAs == "MANEUVER":
            case "Chyna Interferes" when play.PlayedAs == "MANEUVER":
            case "Clean Break" when play.CardInfo.Title == "Jockeying for Position":
            case "Jockeying for Position" when play.CardInfo.Title == "Jockeying for Position":
                return true;
            default:
                return false;
        }
    }

    private static bool ContainsSubtype(string subtype, Card card)
    {
        return card.CardInfo().Subtypes.Contains(subtype);
    }

    public static void AddEffect(Card cardReversing, int damage)
    {
        switch (cardReversing.Title())
        {
            case "Rolling Takedown":
            case "Knee to the Gut":
                cardReversing.AddEffect(new OpponentDamage(damage));
                break;
        }
    }
}