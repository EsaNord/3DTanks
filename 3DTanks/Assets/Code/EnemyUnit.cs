using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tanks3D.AI;
using System.Linq;

namespace Tanks3D
{
    public class EnemyUnit : Unit
    {
        private IList<AIStateBase> _states = new List<AIStateBase>();

        public AIStateBase CurrentState { get; private set; }        

        private void InitStates()
        {
           
        }

        public override void Init()
        {
            base.Init();
            InitStates();
        }

        protected override void Update()
        {
            return;
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