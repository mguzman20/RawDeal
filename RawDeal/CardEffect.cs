namespace RawDeal;

public abstract class CardEffect
{
    public abstract void UseEffect();
}

public class ManuverEffect : CardEffect
{
    public override void UseEffect()
    {
        throw new NotImplementedException();
    }
}