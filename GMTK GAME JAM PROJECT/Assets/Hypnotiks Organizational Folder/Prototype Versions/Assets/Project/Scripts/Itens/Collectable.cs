using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private Vector2 limits = new(1.25f, 2f); // Maybe make collectables have bigger limits than normal, that could be fun
        [SerializeField] private float sizeAmount = 1f;
        [SerializeField] private float staminaAmount = 0.5f;
        [SerializeField] private float hitStopAmount = 4f;
        [SerializeField] private bool untieInImpact;
    
        [SerializeField] private UnityEvent OnCollectedEvent;
    
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Only the player ragdoll can hit the collectables not the ballon
            if (!collision.CompareTag(PlayerController.Instance.PlayerTag))
            {
                return;
            }
    
            BaloonController.Instance.IncreaseSize(sizeAmount, limits);
            PlayerController.Instance.AddStamina(staminaAmount);
    
            if (untieInImpact)
            {
                Rope.Instance.CheckRope(true);
            }
    
            OnCollectedEvent?.Invoke();
    
            HitStop.Instance.Perform(hitStopAmount);
    
            Destroy(gameObject);
        }
    }

}
