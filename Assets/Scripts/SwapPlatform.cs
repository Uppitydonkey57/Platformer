using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPlatform : MonoBehaviour
{
    Collider2D collider2d;

    SpriteRenderer spriteRenderer;

    public Color initialColor = Color.white;

    public Color color = Color.grey;

    public float platformTime;
    public float noPlatformTime;

    public bool startOn = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        collider2d = GetComponent<Collider2D>();

        if (startOn)
            StartCoroutine(Swap());
        else
            StartCoroutine(SwapReverse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Swap()
    {
        while (true)
        {
            collider2d.enabled = true;

            spriteRenderer.color = initialColor;

            yield return new WaitForSeconds(platformTime);

            collider2d.enabled = false;

            spriteRenderer.color = color;

            yield return new WaitForSeconds(noPlatformTime);
        }
    }

    IEnumerator SwapReverse()
    {
        while (true)
        {
            collider2d.enabled = false;

            spriteRenderer.color = color;

            yield return new WaitForSeconds(noPlatformTime);

            collider2d.enabled = true;

            spriteRenderer.color = initialColor;

            yield return new WaitForSeconds(platformTime);
        }
    }
}
