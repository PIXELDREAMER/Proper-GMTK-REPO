using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Game.Core
{
    public class PauseController : GenericSingleton<PauseController>
    {
        [SerializeField]private UnityEvent OnGamePaused;
        [SerializeField]private UnityEvent OnGameResumed;

        private bool _isGamePaused;

        public bool IsGamePaused { get => _isGamePaused; }

        private void Start() 
        {
            OnGamePaused.AddListener(Pause);
            OnGameResumed.AddListener(Resume);    
        }
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(Time.timeScale == 1f)
                {

                    OnGamePaused?.Invoke();
                }
                else
                {

                    OnGameResumed?.Invoke();
                }
            }    
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            _isGamePaused = true;
        }


        public void Resume()
        {
            Time.timeScale = 1f;
            _isGamePaused = false;
        }

        private void OnDestroy() 
        {
            OnGamePaused.RemoveAllListeners();    
        }
    }

}
