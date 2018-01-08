using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public abstract class Unit : MonoBehaviour
    {
        private IMover _mover;

        protected IMover Mover
        {
            get { return _mover; }
        }


        public virtual void Init()
        {
            _mover = gameObject.GetOrAddComponent<TransformMover>();                     
        }

        public virtual void Clear()
        {

        }

        protected abstract void Update();
    }
}