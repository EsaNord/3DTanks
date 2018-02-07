using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField]
        private float m_fAngle;
        [SerializeField]
        private float m_fDistance;
        [SerializeField]
        private Transform m_tTarget;

        private CameraFollow cameraFollow;

        private void Awake()
        {
            cameraFollow = GetComponent<CameraFollow>();                                
        }

        // Update is called once per frame
        void Update()
        {
            cameraFollow.SetTarget(m_tTarget);
            cameraFollow.SetAngle(m_fAngle);
            cameraFollow.SetDistance(m_fDistance);            
            Debug.Log("Distance: " + Vector3.Distance(transform.position, m_tTarget.position));
            Debug.Log("Player rotation:" + m_tTarget.rotation);
        }
    }
}