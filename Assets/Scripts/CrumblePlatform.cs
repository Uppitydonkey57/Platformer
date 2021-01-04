using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblePlatform : MonoBehaviour
{
    public Color crumbledColor;
    Color initialColor;

    public float stayTime;

    public bool shouldRestore = true;
    public float timeUntilRestore;

    SpriteRenderer spriteRenderer;

    public Collider2D collide;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialColor = spriteRenderer.color;

        //collide = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("COLLIDE");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("COLLIDE2");
            StartCoroutine(WaitForBreak());
        }
    }

    IEnumerator WaitForBreak()
    {
        yield return new WaitForSeconds(stayTime);

        collide.enabled = false;

        spriteRenderer.color = crumbledColor;

        if (shouldRestore)
        {
            StartCoroutine(WaitTillRestore());
        }
    }

    IEnumerator WaitTillRestore()
    {
        yield return new WaitForSeconds(timeUntilRestore);

        collide.enabled = true;

        spriteRenderer.color = initialColor;
    }
}
