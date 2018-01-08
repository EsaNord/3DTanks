using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class PlayerUnit : Unit
    {
        [SerializeField]
        private float _moveSpeed = 5f;
        [SerializeField]
        private float _turnSpeed = 5f;
        [SerializeField]
        private const string horizontalAxis = "Horizontal";
        [SerializeField]
        private const string verticalAxis = "Vertical";

        private void Start()
        {
            Init();
            Mover.Init(_moveSpeed, _turnSpeed);
        }

        protected override void Update()
        {
            var _input = ReadInput();
            Mover.Move(_input);
        }

        private Vector3 ReadInput()
        {
            return new Vector3(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis), 0f);
        }
    }
}