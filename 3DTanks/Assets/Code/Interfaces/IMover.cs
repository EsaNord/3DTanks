using UnityEngine;

namespace Tanks3D
{
    public interface IMover
    {
        void Init(float moveSpeed, float turnSpeed);
        
        void Move(Vector3 input);
    }
}