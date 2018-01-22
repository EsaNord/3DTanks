using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D.Testing
{
    public class GetComponentInHierarchyTester : MonoBehaviour
    {
        private bool _inCludeInacive;

        public void Setup(bool inCludeInacive, bool componentInParent, bool setActive)
        {
            _inCludeInacive = inCludeInacive;
            GameObject go;

            if (componentInParent)
            {
                go = transform.parent.gameObject;                
            }
            else
            {
                go = transform.GetChild(0).gameObject;                
            }

            go.AddComponent<TestComponent>();
            go.SetActive(setActive);
        }

        public TestComponent Run()
        {
            return gameObject.GetComponentInHierarchy<TestComponent>(_inCludeInacive);
        }
    }
}