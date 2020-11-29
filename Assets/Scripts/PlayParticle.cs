using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticle : MonoBehaviour
{
    public ParticleSystem particle;

    public void ParticlePlay()
    {
        if (particle != null)
            particle.Play();
    }
}
