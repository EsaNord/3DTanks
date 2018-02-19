namespace Tanks3D.persistance
{
    public interface IPersistance
    {
        // File extension of the save file.
        string Extension { get; }  

        // The path of the save file on the disk.
        string FilePath { get; }

        // A generic method for serializing the game data and storing it in the disk.
        void Save<T>(T data);

        // A generic method for readin searialized data from the disk.
        T Load<T>();
    }
}