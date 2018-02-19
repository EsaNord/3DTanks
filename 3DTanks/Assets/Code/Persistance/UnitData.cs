﻿using System;

namespace Tanks3D.persistance
{
    [Serializable]
    public class UnitData
    {
        public int Id;
        public int Health;
        public SerializableVector3 Position;
        public float YRotation;
    }
}