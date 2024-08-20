using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    
    public class CameraZoomTrigger : MonoBehaviour
    {
        [SerializeField]private CameraZoom cameraZoom;

        [Min(1.0f)]
        [SerializeField]private float enterRadius = 10.0f;
        
        [Min(1.0f)]
        [SerializeField]private float exitRadius = 5.0f;
        
        [SerializeField]private LayerMask detectMask;

        private void OnTriggerEnter2D(Collider2D other) 
        {
            Debug.Log("Something entered the collider: " + other.transform.name);
            int otherLayer = 1 << other.transform.gameObject.layer;
            if((detectMask & otherLayer) != 0)
            {
                cameraZoom.FinalRadius = enterRadius;
            }    
        }

        private void OnTriggerExit2D(Collider2D other) 
        {
            Debug.Log("Something exit the collider: " + other.transform.name);
            int otherLayer = 1 << other.transform.gameObject.layer;
            if((detectMask & otherLayer) != 0)
            {
                cameraZoom.FinalRadius = exitRadius;
            }    
        }    
    }
}
