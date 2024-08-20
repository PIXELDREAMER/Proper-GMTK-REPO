using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviourSingleton<HitStop>
{
    public Coroutine freezeCorountine;

    public void Perform(float duration)
    {
        if (freezeCorountine != null)
        {
            StopCoroutine(freezeCorountine);
        }

        freezeCorountine = StartCoroutine(Freeze(duration));
    }

    private IEnumerator Freeze(float duration, float pausedScale = 0f)
    {
        float originalScale = Time.timeScale;
        //Time.timeScale = pausedScale; // for some reason after we pause the scale it doesnt returns back
        // Use WaitForSecondsRealtime directly with the duration
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = originalScale;
    }

}
