using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Game.Core
{
    public class PauseController : MonoBehaviourSingleton<PauseController>
    {
        // [hypnotik] not sure why we have unity events on this script as we should have event handlers that invoke and then other scripts can subscribe to that event instead of the pause controller doing everything
        // [hypnotik] I will add a pause script from a old game of mine and we could use it as a base
        // [hypnotik] Also, a personal preference, I think we should name it PauseManager (or directly add pausing logic to the game manager) as manager refers when a controller is also a singleton

        [SerializeField] private UnityEvent OnGamePaused;
        [SerializeField] private UnityEvent OnGameResumed;

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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 1f)
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
