namespace Tanks3D.Messaging
{
    public class UnitDiedMessage : IMessage
    {
        public Unit DeadUnit { get; private set; }

        public UnitDiedMessage (Unit unit)
        {
            DeadUnit = unit;
        }
    }
}