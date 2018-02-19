using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Tanks3D.persistance
{
    public class BinaryPersistance : IPersistance
    {
        public string Extension { get { return "/bin"; } }

        public string FilePath { get; private set; }

        /// <summary>
        /// Initializes the BinaryPersistance object.
        /// </summary>
        /// <param name="path">The path of the save file without the extension</param>
        public BinaryPersistance(string path)
        {
            FilePath = path + Extension;
        }

        public void Save<T>(T data)
        {
            using (FileStream stream = File.OpenWrite(FilePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, data);
                stream.Close();
            }
        }

        public T Load<T>()
        {
            T data = default(T);
            try
            {
                using (FileStream stream = File.OpenRead(FilePath))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    data = (T)bf.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return data;
        }
    }
}