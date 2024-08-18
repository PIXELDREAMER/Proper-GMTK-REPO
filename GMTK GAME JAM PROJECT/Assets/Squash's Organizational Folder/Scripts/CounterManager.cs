using System;
using UnityEngine;

namespace Game.Core
{
    public class CounterManager : GenericSingleton<CounterManager>
    {
        public Action<int> OnCounterIncreased;
        private int _currentCount = 0;

        private void IncreaseCount(int amount)
        {
            _currentCount += amount;
        }
        protected override void Awake() 
        {
            base.Awake();
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
