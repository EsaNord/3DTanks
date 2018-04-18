using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks3D.WaypointSystem;
using Tanks3D.Systems;

namespace Tanks3D.AI
{
    public class PatrolState : AIStateBase
    {
        private Path _path;
        private Direction _direction;
        private float _arriveDistance;

        public Waypoint CurrentWaypoint { get; private set; }

        public PatrolState(EnemyUnit owner, Path path, Direction direction, float arriveDistance) : base()
        {
            State = AIStateType.Patrol;
            Owner = owner;
            AddTransition(AIStateType.FollowTarget);
            _path = path;
            _direction = direction;
            _arriveDistance = arriveDistance;
        }

        public override void StateActivated()
        {
            base.StateActivated();
            CurrentWaypoint = _path.GetClosestWaypoint(Owner.transform.position);
            Debug.Log("Patrol");
        }

        public override void Update()
        {
            // 1. should we change the state?
            // 1.1 if yes, change state and return
            if (!ChangeState())
            {
                // 2. are we close enough the current waypoint?
                // 2.1 if yes, get next waypoint
                CurrentWaypoint = GetWaypoint();
                // 3. move towards current waypoint                
                Owner.Mover.Move(Owner.transform.forward);
                Owner.Mover.Turn(CurrentWaypoint.Position);
                // 4. rotate towards current waypoint
            } 
        }

        private Waypoint GetWaypoint()
        {
            Waypoint result = CurrentWaypoint;

            Vector3 toWaypointVector = CurrentWaypoint.Position - Owner.transform.position;
            float toWaypointSqr = toWaypointVector.sqrMagnitude;
            float sqrArriveDistance = _arriveDistance * _arriveDistance;

            if (toWaypointSqr <= sqrArriveDistance)
            {
                result = _path.GetNextWaypoint(CurrentWaypoint, ref _direction);
            }

            return result;
        }

        private bool ChangeState()
        {
            //int mask = LayerMask.GetMask("Player");
            int playerLayer = LayerMask.NameToLayer("Player");
            int mask = Flags.CreateMask(playerLayer);

            Collider[] players = Physics.OverlapSphere(Owner.transform.position, Owner.DetecEnemyDistance, mask);

            if (players.Length > 0)
            {
                PlayerUnit player = players[0].gameObject.GetComponentInHierarchy<PlayerUnit>();

                if (player != null)
                {
                    Owner.Target = player;
                    float sqrDistanceToPlayer = Owner.ToTargetVector.Value.sqrMagnitude;
                    if (sqrDistanceToPlayer < Owner.DetecEnemyDistance * Owner.DetecEnemyDistance)
                        return Owner.PerformTransition(AIStateType.FollowTarget);

                    Owner.Target = null;
                }                            
            }
            return false;
        }
    }
}