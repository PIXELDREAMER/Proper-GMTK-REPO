using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    private void Awake()
    {
        if (!TryGetComponent(out Animator animator))
        {
            Debug.LogError("Eyes don't have animator component");
            return;
        }

        float randomValue = Random.Range(0f, 2f);
        //Debug.Log("random eye offset" + randomValue);

        animator.SetFloat("Cycle Offset", randomValue);
    }
}
