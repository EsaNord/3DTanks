using Tanks3D.persistance;
using UnityEngine;

namespace Tanks3D.Testing
{
    public class OperatorTesting : MonoBehaviour
    {       
        private void Start()
        {
            var first = new SerializableVector3(1, 2, 3);
            var second = new Vector3(3, 2, 1);

            var result = first + second;
            //var result2 = first - second;
            Debug.Log(result);            
            Debug.Log(-first);
        }
    }
}