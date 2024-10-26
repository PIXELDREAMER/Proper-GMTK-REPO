using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class SlowdownTrigger : MonoBehaviour
    {
        [SerializeField]private LayerMask detectMask;
        [SerializeField]private float pickupSpeed = 5.0f;
        [Range(0.1f, 1.0f)]
        [SerializeField]private float slowTimeScale = 0.1f;
        private float currentTimeScale = 1.0f;
        private float finalTimeScale = 1.0f;
        private void Update() 
        {
            currentTimeScale = Mathf.Lerp(currentTimeScale, finalTimeScale, pickupSpeed * Time.deltaTime);
            Time.timeScale = currentTimeScale;
        }
        private void OnTriggerEnter2D(Collider2D other) 
        {
            int otherLayer = 1 << other.transform.gameObject.layer;
            if((detectMask & otherLayer) != 0)
            {
                finalTimeScale = slowTimeScale;    
            }    
        }

        private void OnTriggerExit2D(Collider2D other) 
        {
            Debug.Log("Something exit the collider: " + other.transform.name);
            int otherLayer = 1 << other.transform.gameObject.layer;
            if((detectMask & otherLayer) != 0)
            {
                finalTimeScale = 1.0f;
            }    
        }
    }
}
