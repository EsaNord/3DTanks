namespace Tanks3D.Messaging
{
    public class UnitGotHitMessage : IMessage
    {
        public Unit DamageUnit { get; private set; }

        public UnitGotHitMessage(Unit unit)
        {
            DamageUnit = unit;
        }
    }
}