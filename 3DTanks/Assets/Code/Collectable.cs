using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField, Tooltip("Points")]
        private int _points = 50;
        [SerializeField, Tooltip("Collectables rotation speed")]
        private float _rotationSpeed = 200f;

        private Score _score;
        private System.Action<Collectable> _collisionCallback;

        /// <summary>
        /// Initalization method for collectable      
        /// </summary>
        /// <param name="score"> Reference to Score script</param>
        /// <param name="collisionCallback">Action reference for collectable collection</param>
        public void Init(Score score, System.Action<Collectable> collisionCallback)
        {
            _score = score;
            _collisionCallback = collisionCallback;
        }

        /// <summary>
        /// Checks when object collides with collectable
        /// and triggers collisionCallback event and increases players score.
        /// </summary>
        /// <param name="collision"> Object that collided</param>
        private void OnCollisionEnter(Collision collision)
        {
            _score.CurrentScore += _points;
            Debug.Log("Collected");
            _collisionCallback(this);
        }

        private void Update()
        {
            // for nice rotating animation for collectable
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
    }
}