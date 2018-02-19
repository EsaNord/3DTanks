using System;
using System.Collections.Generic;

namespace Tanks3D.persistance
{
    [Serializable]
    public class GameData 
    {
        public UnitData PlayerData;
        public List<UnitData> EnemyData = new List<UnitData>();
    }
}