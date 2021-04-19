using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForDestroyedObjects : MonoBehaviour
{
    public GameObject[] waitObjects;

    public ParticleSystem particle;

    SpriteRenderer rend;

    bool destroyed;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        ParticleSystem.ShapeModule shape = particle.shape;

        Vector2 scale = rend.size;

        //particle.emission.rateOverTime = particle.emission.rateOverTime * (rend.size / 100);

        shape.scale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject gameObject in waitObjects)
        {
            if (gameObject != null)
            {
                return;
            }
        }

        if (!destroyed)
        {

            if (particle != null)
            {
                particle.Play();
            }

            GetComponent<BoxCollider2D>().enabled = false;
            rend.enabled = false;

            destroyed = true;
        }
    }
}
