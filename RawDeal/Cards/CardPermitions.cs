using RawDeal.Info;

namespace RawDeal.Cards;

public static class CardPermitions
{
    public static bool IsPermited(PlayInfo play, PlayInfo lastPlay)
    {
        switch (play.CardInfo.Title)
        {
            case "Back Body Drop":
                return lastPlay.CardInfo.Title == "Irish Whip";
            case "Shoulder Block":
                return lastPlay.CardInfo.Title == "Irish Whip" && lastPlay.PlayedAs == "MANEUVER";
            case "Cross Body Block":
                return true;
            case "Ensugiri":
                return lastPlay.CardInfo.Title == "Kick";
            case "Drop Kick":
                return lastPlay.CardInfo.Title == "Drop Kick";
            case "Discus Punch":
                return true;
            case "Superkick":
                return true;
            case "Spear":
                return lastPlay.CardInfo.Title == "Irish Whip" && lastPlay.PlayedAs == "MANEUVER";
            case "Belly to Belly Suplex":
                return lastPlay.CardInfo.Title == "Belly to Belly Suplex" && lastPlay.PlayedAs == "MANEUVER";
            case "Vertical Suplex":
                return lastPlay.CardInfo.Title == "Vertical Suplex" && lastPlay.PlayedAs == "MANEUVER";
            case "Belly to Back Suplex":
                return lastPlay.CardInfo.Title == "Belly to Back Suplex" && lastPlay.PlayedAs == "MANEUVER";
            case "Rolling Takedown":
                return true;
            case "Knee to the Gut":
                return true;
            case "Elbow to the Face":
                return true;
            case "Clean Break":
                return true;
            case "Manager Interferes":
                return true;
            case "Disqualification!":
                return true;
            case "No Chance in Hell":
                return true;
            case "Jockeying for Position":
                return true;
            case "Irish Whip":
                return true;
            case "Flash in the Pan":
                return true;
            case "View of Villainy":
                return true;
            case "Shake It Off":
                return true;
            case "Roll Out of the Ring":
                return true;
            case "Distract the Ref":
                return true;
            case "Recovery":
                return true;
            case "Spit At Opponent":
                return true;
            case "Get Crowd Support":
                return true;
            case "Comeback!":
                return true;
            case "Ego Boost":
                return true;
            case "Stagger":
                return true;
            case "Diversion":
                return true;
            case "Marking Out":
                return true;
            case "Puppies! Puppies!":
                return true;
            case "Shane O'Mac":
                return true;
            case "Maintain Hold":
                return true;
            case "Pat & Gerry":
                return true;
            case "Austin Elbow Smash":
                return true;
            case "Lou Thesz Press":
                return true;
            case "Double Digits":
                return true;
            case "Stone Cold Stunner":
                return true;
            case "Open Up a Can of Whoop-A%$":
                return true;
            case "Undertaker's Chokeslam":
                return true;
            case "Undertaker's Flying Clothesline":
                return true;
            case "Undertaker Sits Up!":
                return true;
            case "Undertaker's Tombstone Piledriver":
                return true;
            case "Power of Darkness":
                return true; case "Have a Nice Day!":
                return true;
            case "Double Arm DDT":
                return true;
            case "Tree of Woe":
                return true;
            case "Mandible Claw":
                return true;
            case "Mr. Socko":
                return true;
            case "Leaping Knee to the Face":
                return lastPlay.CardInfo.Title == "Irish Whip";
            case "Facebuster":
                return true;
            case "I Am the Game":
                return true;
            case "Pedigree":
                return true;
            case "Chyna Interferes":
                return true;
            case "Smackdown Hotel":
                return true;
            case
                "Take That Move, Shine It Up Real Nice, Turn That Sumb*tch Sideways, and Stick It Straight Up Your Roody Poo Candy A%$!"
                :
                return true;
            case "Rock Bottom":
                return true;
            case "The People's Eyebrow":
                return true;
            case "The People's Elbow":
                return true;
            case "Kane's Chokeslam":
                return true;
            case "Kane's Flying Clothesline":
                return true;
            case "Kane's Return!":
                return true;
            case "Kane's Tombstone Piledriver":
                return true;
            case "Lionsault":
                return lastPlay.Damage() >= 4 && lastPlay.PlayedAs == "MANEUVER";
            case "Don't You Never... EVER!":
                return true;
            case "Walls of Jericho":
                return true;
            default:
                return true;
        }
    }
}