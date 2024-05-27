namespace RawDeal.Cards;

internal static class CardOptions
{
    public static void GetEffectsList(Card card, string cardName)
    {
        switch (cardName)
        {
            case "Chop":
                card.AddEffect(new DiscardCard(card));
                card.AddEffect(new PlayerDrawCard(1));
                break;
            case "Punch":
                break;
            case "Head Butt":
                card.AddEffect(new PlayerDiscardCard(1));
                break;
            case "Roundhouse Punch":
                break;
            case "Haymaker":
                break;
            case "Back Body Drop":
                card.AddEffect(new ChoseBetweenDrawOrDiscard(2));
                break;
            case "Big Boot":
                break;
            case "Shoulder Block":
                break;
            case "Kick":
                card.AddEffect(new PLayerDamageHimself());
                break;
            case "Cross Body Block":
                break;
            case "Ensugiri":
                break;
            case "Running Elbow Smash":
                card.AddEffect(new PLayerDamageHimself());
                break;
            case "Drop Kick":
                break;
            case "Discus Punch":
                break;
            case "Superkick":
                break;
            case "Spinning Heel Kick":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Spear":
                break;
            case "Clothesline":
                break;
            case "Chair Shot":
                break;
            case "Hurricanrana":
                break;
            case "Arm Bar Takedown":
                card.AddEffect(new DiscardCard(card));
                card.AddEffect(new PlayerDrawCard(1));
                break;
            case "Hip Toss":
                break;
            case "Arm Drag":
                card.AddEffect(new PlayerDiscardCard(1));
                break; 
            case "Russian Leg Sweep":
                break;
            case "Snap Mare":
                break;
            case "Gut Buster":
                break;
            case "Body Slam":
                break;
            case "Back Breaker":
                break;
            case "Double Leg Takedown":
                card.AddEffect(new PlayerMayDrawCard(1));
                break;
            case "Fireman's Carry":
                break;
            case "Headlock Takedown":
                card.AddEffect(new OpponentDrawCard(1));
                break;
            case "Belly to Belly Suplex":
                break;
            case "Atomic Facebuster":
                break;
            case "Atomic Drop":
                break;
            case "Inverse Atomic Drop":
                break;
            case "Vertical Suplex":
                break;
            case "Belly to Back Suplex":
                break;
            case "Pump Handle Slam":
                card.AddEffect(new OpponentDiscardCard(2));
                break;
            case "Reverse DDT":
                card.AddEffect(new PlayerMayDrawCard(1));
                break;
            case "Samoan Drop":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Sit Out Powerbomb":
                break;
            case "Bulldog":
                card.AddEffect(new PlayerDiscardCard(1));
                card.AddEffect(new PlayerDiscardCardFromOpponent(1));
                break;
            case "Fisherman's Suplex":
                card.AddEffect(new PLayerDamageHimself());
                card.AddEffect(new PlayerMayDrawCard(1));
                break;
            case "DDT":
                card.AddEffect(new PLayerDamageHimself());
                card.AddEffect(new OpponentDiscardCard(2));
                break;
            case "Power Slam":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Powerbomb":
                break;
            case "Press Slam":
                card.AddEffect(new PLayerDamageHimself());
                card.AddEffect(new OpponentDiscardCard(2));
                break;
            case "Collar & Elbow Lockup":
                card.AddEffect(new DiscardCard(card));
                card.AddEffect(new PlayerDrawCard(1));
                break;
            case "Wrist Lock":
                break;
            case "Arm Bar":
                card.AddEffect(new PlayerDiscardCard(1));
                break;
            case "Chin Lock":
                break;
            case "Bear Hug":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Full Nelson":
                break;
            case "Choke Hold":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Step Over Toe Hold":
                break;
            case "Ankle Lock":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Standing Side Headlock":
                card.AddEffect(new OpponentDrawCard(1));
                break;
            case "Cobra Clutch":
                break;
            case "Bow & Arrow":
                break;
            case "Chicken Wing":
                card.AddEffect(new RecoverDamage(2));
                break;
            case "Sleeper":
                break;
            case "Camel Clutch":
                break;
            case "Boston Crab":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Guillotine Stretch":
                card.AddEffect(new OpponentDiscardCard(1));
                card.AddEffect(new PlayerMayDrawCard(1));
                break;
            case "Abdominal Stretch":
                break;
            case "Torture Rack":
                card.AddEffect(new OpponentDiscardCard(1));
                break; 
            case "Figure Four Leg Lock":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Step Aside":
                break;
            case "Escape Move":
                break;
            case "Break the Hold":
                break;
            case "Rolling Takedown":
                break;
            case "Knee to the Gut":
                break;
            case "Elbow to the Face":
                card.AddEffect(new OpponentDamage(2));
                break;
            case "Clean Break":
                card.AddEffect(new OpponentDiscardCard(4));
                card.AddEffect(new PlayerDrawCard(1));
                break;
            case "Manager Interferes":
                card.AddEffect(new PlayerDrawCard(1));
                card.AddEffect(new OpponentDamage(1));
                break;
            case "Disqualification!":
                break;
            case "No Chance in Hell":
                break;
            case "Hmmm":
                break;
            case "Don't Think Too Hard":
                break;
            case "Whaddya Got?":
                break;
            case "Not Yet":
                break;
            case "Jockeying for Position":
                card.AddEffect(new JockeyingForPosition(card));
                break;
            case "Irish Whip":
                break;
            case "Flash in the Pan":
                break;
            case "View of Villainy":
                break;
            case "Shake It Off":
                break;
            case "Offer Handshake":
                card.AddEffect(new PlayerMayDrawCard(3));
                card.AddEffect(new PlayerDiscardCard(1));
                break;
            case "Roll Out of the Ring":
                break;
            case "Distract the Ref":
                break;
            case "Recovery":
                card.AddEffect(new RecoverDamage(2));
                card.AddEffect(new PlayerMayDrawCard(1));
                break;
            case "Spit At Opponent":
                card.AddEffect(new PlayerDiscardCard(1));
                card.AddEffect(new OpponentDiscardCard(4));
                break;
            case "Get Crowd Support":
                break;
            case "Comeback!":
                break;
            case "Ego Boost":
                break;
            case "Deluding Yourself":
                break;
            case "Stagger":
                break;
            case "Diversion":
                break;
            case "Marking Out":
                break;
            case "Puppies! Puppies!":
                card.AddEffect(new RecoverDamage(5));
                card.AddEffect(new PlayerMayDrawCard(2));
                break;
            case "Shane O'Mac":
                break;
            case "Maintain Hold":
                break;
            case "Pat & Gerry":
                break;
            case "Austin Elbow Smash":
                break;
            case "Lou Thesz Press":
                break;
            case "Double Digits":
                break;
            case "Stone Cold Stunner":
                break;
            case "Open Up a Can of Whoop-A%$":
                break;
            case "Undertaker's Chokeslam":
                break;
            case "Undertaker's Flying Clothesline":
                break;
            case "Undertaker Sits Up!":
                break;
            case "Undertaker's Tombstone Piledriver":
                break;
            case "Power of Darkness":
                break;
            case "Have a Nice Day!":
                break;
            case "Double Arm DDT":
                break;
            case "Tree of Woe":
                break;
            case "Mandible Claw":
                break;
            case "Mr. Socko":
                break;
            case "Leaping Knee to the Face":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Facebuster": 
                break;
            case "I Am the Game":
                card.AddEffect(new ChoseBetweenDrawOrDiscard(2));
                break;
            case "Pedigree":
                break;
            case "Chyna Interferes":
                card.AddEffect(new PlayerDrawCard(2));
                card.AddEffect(new OpponentDamage(3));
                break;
            case "Smackdown Hotel":
                break;
            case
                "Take That Move, Shine It Up Real Nice, Turn That Sumb*tch Sideways, and Stick It Straight Up Your Roody Poo Candy A%$!"
                :
                break;
            case "Rock Bottom":
                break;
            case "The People's Eyebrow":
                break;
            case "The People's Elbow":
                break;
            case "Kane's Chokeslam":
                break;
            case "Kane's Flying Clothesline":
                break;
            case "Kane's Return!":
                break;
            case "Kane's Tombstone Piledriver":
                break;
            case "Hellfire & Brimstone":
                break;
            case "Lionsault":
                card.AddEffect(new OpponentDiscardCard(1));
                break;
            case "Y2J":
                card.AddEffect(new ChoseBetweenDrawOrDiscard(5));
                break;
            case "Don't You Never... EVER!":
                break;
            case "Walls of Jericho":
                break;
            case "Ayatollah of Rock 'n' Roll-a":
                break;
        }
    }
}