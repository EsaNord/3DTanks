namespace Tanks3D.Messaging
{
    public class UnitReset : IMessage
    {
        public Unit ResetedUnit { get; private set; }

        public UnitReset(Unit unit)
        {
            ResetedUnit = unit;
        }
    }
}