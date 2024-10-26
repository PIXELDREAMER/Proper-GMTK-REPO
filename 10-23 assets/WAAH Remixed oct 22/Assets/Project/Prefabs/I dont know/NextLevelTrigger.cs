using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

    public class NextLevelTrigger : MonoBehaviour
    {
        public SceneLoader sceneLoader; 
        public string nextLevelName;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
          sceneLoader.LoadScene(nextLevelName);
        }
 
      
    }

