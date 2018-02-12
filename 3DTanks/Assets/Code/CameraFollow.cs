using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class CameraFollow : MonoBehaviour, ICameraFollow
    {
        private float m_fAngle;
        private float m_fDistance;
        private Transform m_tTarget;        
        
        public void SetAngle(float angle)
        {
            m_fAngle = angle;            
        }

        public void SetDistance(float distance)
        {
            m_fDistance = distance;
        }

        public void SetTarget(Transform targetTransform)
        {
            m_tTarget = targetTransform;            
        }

        private void LateUpdate()
        {
            //transform.rotation = Quaternion.Euler(m_fAngle, 0, 0);
            //transform.position = m_tTarget.position;
            //transform.position -= transform.forward * m_fDistance;

            //
            float angle = Mathf.Deg2Rad * m_fAngle;
            float horizontal = Mathf.Sin(angle) * m_fDistance;
            float y = Mathf.Cos(angle) * m_fDistance;

            // Set Cameras position to be same as targets location
            Vector3 cameraPos = m_tTarget.position;

            // 
            Vector3 direction = m_tTarget.forward;
            direction.y = 0;
            direction.Normalize();
            direction = -direction * horizontal;

            //
            cameraPos.x += direction.x;
            cameraPos.y += y;
            cameraPos.z += direction.z;

            // Apply modified position to camera
            transform.position = cameraPos;
            transform.LookAt(m_tTarget);
        }  
    }
}