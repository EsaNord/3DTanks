using UnityEngine;
using System;

namespace Tanks3D
{
    public class Score : MonoBehaviour
    {        
        [SerializeField, Tooltip("Target points")]
        private int _targetScore;

        private int _score;

        public event Action<int> ScoreChanged;

        // returns and sets current score.
        public int CurrentScore
        {
            get { return _score; }
            set
            {
                _score = value;
                if (ScoreChanged != null)
                {
                    ScoreChanged(_score);
                }
            }
        }     
        
        // returns score amount that is needed to win.
        public int TargetScore { get { return _targetScore; } }        
    }
}