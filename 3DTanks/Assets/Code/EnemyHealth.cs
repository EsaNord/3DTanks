using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class EnemyHealth : Health
    {
        public EnemyHealth(Unit owner, int startingHealth) : base(owner, startingHealth)
        {
        }
    }
}