using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float sizeAmount = 1f;

    [SerializeField] private UnityEvent OnCollectedEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        int a = Random.Range(0, 2);
        float e = a == 1 ? 1 : -1;
        float rand = e * sizeAmount;

        BaloonController.Instance.IncreaseSize(rand);
        OnCollectedEvent?.Invoke();

        Destroy(gameObject);
    }
}
