using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Tanks3D.persistance
{
    public class BinaryPersistance : IPersistance
    {
        public string Extension { get { return ".bin"; } }

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
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            using (FileStream stream = File.OpenWrite(FilePath))
            {
                BinaryFormatter bf = new BinaryFormatter();

                var surrogateSelector = new SurrogateSelector();
                Vector3Surrogate v3S = new Vector3Surrogate();
                surrogateSelector.AddSurrogate(typeof(Vector3),
                    new StreamingContext(StreamingContextStates.All), v3S);
                bf.SurrogateSelector = surrogateSelector;

                bf.Serialize(stream, data);
                stream.Close();
            }
        }

        public T Load<T>()
        {
            T data = default(T);
            if (File.Exists(FilePath))
            {
                FileStream stream = File.OpenRead(FilePath);

                try
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    var surrogateSelector = new SurrogateSelector();
                    Vector3Surrogate v3S = new Vector3Surrogate();
                    surrogateSelector.AddSurrogate(typeof(Vector3),
                        new StreamingContext(StreamingContextStates.All), v3S);
                    bf.SurrogateSelector = surrogateSelector;

                    data = (T)bf.Deserialize(stream);
                }
                catch (SerializationException e)
                {
                    Debug.LogError("Serialization Failed");
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
                finally
                {
                    stream.Close();
                }
            }
            return data;
        }
    }
}