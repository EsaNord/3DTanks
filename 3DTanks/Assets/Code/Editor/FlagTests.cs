using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Tanks3D.Systems;

namespace Tanks3D.Testing
{
    public class FlagTests
    {
        [Test]
        public void FlagTestCreatePlayerAndEnemyMask()
        {
            int playerLayer = LayerMask.NameToLayer("Player");
            int enemyLayer = LayerMask.NameToLayer("Enemy");

            int mask = Flags.CreateMask(playerLayer, enemyLayer);
            int validMask = LayerMask.GetMask("Player", "Enemy");

            Assert.AreEqual(mask, validMask);
        }
    }
}