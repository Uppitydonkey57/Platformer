using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForDestroyedObjects : MonoBehaviour
{
    public GameObject[] waitObjects;

    // Start is called before the first frame update
    void Start()
    {
        
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

        Destroy(gameObject);
    }
}
