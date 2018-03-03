using System.Collections;
using System.Collections.Generic;
using Tanks3D.Systems;
using UnityEngine;

namespace Tanks3D.AI
{
    public class ShootState : AIStateBase
    {
        public float SqrShootingDistance { get { return Owner.ShootingDistance * Owner.ShootingDistance; } }

        /// <summary>
        /// Shoot states builder method.
        /// </summary>
        /// <param name="owner">Scripts owner</param>
        public ShootState (EnemyUnit owner)
            : base(owner, AIStateType.Shoot)
        {
            // Add allowed transitions.
            AddTransition(AIStateType.Patrol);
            AddTransition(AIStateType.FollowTarget);
        }

        /// <summary>
        /// State activation.
        /// </summary>
        public override void StateActivated()
        {
            base.StateActivated();
        }

        /// <summary>
        /// Update method called every frame.
        /// </summary>
        public override void Update()
        {                  
            if (!ChangeState())
            {                
                CheckTarget();
            }
        }

        /// <summary>
        /// Checks if state needs to be changed.
        /// </summary>
        /// <returns>Boolean value</returns>
        private bool ChangeState()
        {
            Vector3 toPlayerVector = Owner.transform.position - Owner.Target.transform.position;
            float sqrDistanceToPlayer = toPlayerVector.sqrMagnitude;

            // If player unit dies or otherwise is no longer in game,
            // current state is changed to patrol state.
            if (Owner.Target == null)
                return Owner.PerformTransition(AIStateType.Patrol);
            
            // If player moves out of shooting range, current state is changed to
            // follow target state.
            if (sqrDistanceToPlayer > SqrShootingDistance)
                return Owner.PerformTransition(AIStateType.FollowTarget);            

            // Otherwise false is returned.
            return false;
        }

        /// <summary>
        /// Checks if target is front of enemy unit.
        /// </summary>
        private void CheckTarget()
        {
            int playerLayer = LayerMask.NameToLayer("Player");
            int mask = Flags.CreateMask(playerLayer);

            // If player is front of the owner unit, owner will fire it's gun.
            if (Physics.Raycast(Owner.transform.position, Owner.transform.forward ,Owner.ShootingDistance, mask))
                Owner.Weapon.Shoot();

            // Otherwise owner continues moveing and turning towards targeted player.
            else
            {
                Owner.Mover.Turn(Owner.Target.transform.position);
                Owner.Mover.Move(Owner.transform.forward);
            }
        }
    }
}