﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class PlayerUnit : Unit
    {
        [SerializeField]
        private string _horizontalAxis = "Horizontal";
        [SerializeField]
        private string _verticalAxis = "Vertical"; 
        
        protected override void Update()
        {
            var input = ReadInput();
            Mover.Turn(input.x);
            Mover.Move(input.z);
            bool shoot = Input.GetButton("Fire1");
            if (shoot)
            {
                Weapon.Shoot();
            }
        }

        private Vector3 ReadInput()
        {
            float movement = Input.GetAxis(_verticalAxis);
            float turn = Input.GetAxis(_horizontalAxis);
            return new Vector3(turn, 0, movement);
        }
    }
}