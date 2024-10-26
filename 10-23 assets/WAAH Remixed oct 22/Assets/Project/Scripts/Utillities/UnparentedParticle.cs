using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If you want spawn particles instead of playing them (good for objects that destroy themselves like coins)
public class UnparentedParticle : MonoBehaviour
{
    private ParticleSystem particle;

    public void PlayParticleUnparented()
    {
        if (particle == null)
        {
            particle = GetComponent<ParticleSystem>();
        }

        var newParticle = Instantiate(particle, particle.transform.position, particle.transform.rotation);
        newParticle.gameObject.SetActive(true);
        newParticle.gameObject.name = particle.name + " (clone)";
        newParticle.Play();
    }
}
