using System;
using UnityEngine;

namespace Game.Core
{
    // [hypnotik] This seens like some sorta of counter, I'm not sure for what it is used for
    public class CounterManager : MonoBehaviourSingleton<CounterManager>
    {
        public Action<int> OnCounterIncreased;
        private int _currentCount = 0;

        private void IncreaseCount(int amount)
        {
            _currentCount += amount;
        }

        //protected override void Awake() 
        private void Awake() 
        {
            //base.Awake();
            if(Instance != null)
            {
                _currentCount = 0;
                DontDestroyOnLoad(gameObject);
            }
        }
        
        private void Start() 
        {
            OnCounterIncreased += IncreaseCount;    
        }

        private void OnDestroy() 
        {
            OnCounterIncreased -= IncreaseCount;    
        }
    }
}
