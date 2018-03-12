using System;
using UnityEngine;

namespace Tanks3D.persistance
{
    [Serializable]
    public class UnitData
    {
        public int Id;
        public int Health;
        public Vector3 Position;
        public float YRotation;
    }
}