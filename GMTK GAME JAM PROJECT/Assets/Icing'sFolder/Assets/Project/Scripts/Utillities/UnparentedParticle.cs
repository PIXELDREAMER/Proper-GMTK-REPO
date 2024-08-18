using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        newParticle.Play();
    }
}
