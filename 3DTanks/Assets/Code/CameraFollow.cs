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

        // TODO have camera rotate with player.

        public void SetAngle(float angle)
        {
            m_fAngle = angle;

            transform.eulerAngles = new Vector3(angle, 0, 0);
        } 

        public void SetDistance(float distance)
        {
            float x = distance * Mathf.Sin(m_fAngle);
            float z = distance * Mathf.Cos(m_fAngle);

            transform.position += new Vector3(0, x, -z);
        }

        public void SetTarget(Transform targetTransform)
        {
            transform.position = targetTransform.position;
        }         
    }
}