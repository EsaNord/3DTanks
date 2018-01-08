using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class TransformMover : MonoBehaviour, IMover
    {
        private float _moveSpeed, _turnSpeed;

        public void Init(float moveSpeed, float turnSpeed)
        {
            _moveSpeed = moveSpeed;
            _turnSpeed = turnSpeed;
        }

        public void Move(Vector3 input)
        {            
            transform.Translate(new Vector3(0f, 0f, input.y) * _moveSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0f, input.x, 0f) * _turnSpeed * Time.deltaTime);
        }        
    }
}