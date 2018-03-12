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
            //UpdatePosition();

            if (m_tTarget != null)
            {
                transform.position = CalculatePosition();
                transform.eulerAngles = CalculateDirection();
            }
        }

        private void UpdatePosition()
        {
            // Does same nearly same thing as below.
            // Sets selected rotation to camera's x rotation and copies targets y rotation.
            // Then center camera to target and moves it back by selected amount. 
            //transform.rotation = Quaternion.Euler(m_fAngle, m_tTarget.rotation.eulerAngles.y, 0);
            //transform.position = m_tTarget.position;
            //transform.position -= transform.forward * m_fDistance;

            // Converts set angle into radians
            // and calculates horizontal and vertical offset.
            float angle = Mathf.Deg2Rad * m_fAngle;
            float horizontal = Mathf.Sin(angle) * m_fDistance;
            float y = Mathf.Cos(angle) * m_fDistance;

            // Set Cameras position to be same as targets position.
            Vector3 cameraPos = m_tTarget.position;

            // Gets target's position, sets y value to 0
            // normalizes vector and adds horizontal offset.
            Vector3 direction = m_tTarget.forward;
            direction.y = 0;
            direction.Normalize();
            direction = -direction * horizontal;

            // Adds position and vertical offset to cameras position.
            cameraPos.x += direction.x;
            cameraPos.y += y;
            cameraPos.z += direction.z;

            // Apply modified position to camera's transform
            // and sets it to look at player.
            transform.position = cameraPos;
            transform.LookAt(m_tTarget);
        }

        private Vector3 CalculatePosition()
        {
            float angle = Mathf.Deg2Rad * m_fAngle;
            float horizontal = Mathf.Sin(angle) * m_fDistance;
            float y = Mathf.Cos(angle) * m_fDistance;

            return m_tTarget.position + m_tTarget.forward * -1 * horizontal + m_tTarget.up * y;
        }

        private Vector3 CalculateDirection()
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.y = m_tTarget.eulerAngles.y;
            rotation.x = 90 - m_fAngle;

            return rotation;
        }
    }
}