using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks3D.Systems;

namespace Tanks3D.AI
{
    public class FollowTargetState : AIStateBase
    {
        public float SqrShootingDistance { get { return Owner.ShootingDistance * Owner.ShootingDistance; } }
        public float SqrDetecPlayerDistance { get { return Owner.DetecEnemyDistance * Owner.DetecEnemyDistance; } }

        public FollowTargetState(EnemyUnit owner)
            :base(owner, AIStateType.FollowTarget)
        {            
            AddTransition(AIStateType.Shoot);
            AddTransition(AIStateType.Patrol);
        }

        public override void StateActivated()
        {
            base.StateActivated();
        }

        public override void Update()
        {
            if (!ChangeState())
            {
                Owner.Mover.Turn(Owner.Target.transform.position);
                Owner.Mover.Move(Owner.transform.forward);
            }
        }

        private bool ChangeState()
        {
            Vector3 toPlayerVector = Owner.transform.position - Owner.Target.transform.position;
            float sqrDistanceToPlayer = toPlayerVector.sqrMagnitude;

            if (sqrDistanceToPlayer < SqrShootingDistance)
                return Owner.PerformTransition(AIStateType.Shoot);

            if (sqrDistanceToPlayer > SqrDetecPlayerDistance)
            {
                Owner.Target = null;
                return Owner.PerformTransition(AIStateType.Patrol);
            }
            
            return false;
        }
    }
}