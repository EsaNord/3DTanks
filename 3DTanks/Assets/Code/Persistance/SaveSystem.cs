namespace Tanks3D.persistance
{
    public class SaveSystem
    {
        private IPersistance _persistance;

        public SaveSystem(IPersistance persistance)
        {
            _persistance = persistance;
        }

        public void Save (GameData data)
        {
            _persistance.Save<GameData>(data);
        }

        public GameData Load()
        {
            return _persistance.Load<GameData>();
        }
    }
}