using System;
using UnityEngine;

namespace Game.Core
{
    public class CounterManager : MonoBehaviour
    {
        public Action<int> OnCounterIncreased;
        private int _currentCount = 0;

        private void IncreaseCount(int amount)
        {
            _currentCount += amount;
        }
        private void Awake() 
        {
            _currentCount = 0;
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
