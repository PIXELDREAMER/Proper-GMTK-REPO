using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Obstacle : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
            {
                return;
            }
    
            Destroy(collision.gameObject);
        }
    }
}
