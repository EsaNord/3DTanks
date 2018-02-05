using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks3D.AI;
using System.Linq;
using Tanks3D.WaypointSystem;

namespace Tanks3D
{
    public class EnemyUnit : Unit
    {
        [SerializeField]
        private float _detecEnemyDistance;
        [SerializeField]
        private float _shootingDistance;
        [SerializeField]
        private float _arriveDistance;
        [SerializeField]
        private Path _path;
        [SerializeField]
        private Direction direction;

        private IList<AIStateBase> _states = new List<AIStateBase>();        

        public AIStateBase CurrentState { get; private set; }     
        public float DetecEnemyDistance { get { return _detecEnemyDistance; } }
        public float ShootingDistance { get { return _shootingDistance; } }
        public PlayerUnit Target { get; set; }        

        private void InitStates()
        {
            PatrolState patrol = new PatrolState(this, _path, direction, _arriveDistance);
            _states.Add(patrol);
            CurrentState = patrol;
            CurrentState.StateActivated();
        }

        public override void Init()
        {
            base.Init();
            InitStates();
        }

        protected override void Update()
        {            
            CurrentState.Update();
        }

        public bool PerformTransition(AIStateType targetState)
        {
            if (!CurrentState.CheckTransition(targetState))
            {
                return false;
            }
            bool result = false;
            AIStateBase state = GetStateByType(targetState);
            if (state != null)
            {
                CurrentState.StateDeactivating();
                CurrentState = state;
                CurrentState.StateActivated();
                result = true;                
            }

            return result;
        }

        private AIStateBase GetStateByType(AIStateType stateType)
        {
            //foreach (AIStateBase state in _states)
            //{
            //    if (state.State == stateType)
            //    {
            //        return state;
            //    }
            //}

            return _states.FirstOrDefault(state => state.State == stateType);
        } 
    }
}