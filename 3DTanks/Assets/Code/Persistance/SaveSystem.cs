namespace Tanks3D.persistance
{
    public class SaveSystem
    {
        private IPersistance _persistance;

        public SaveSystem(IPersistance persistance)
        {
            _persistance = persistance;
        }
    }
}