using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject[] health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateHealth(float currentHealth)
    {
        foreach (GameObject hp in health)
        {
            hp.SetActive(false);
        }

        for (float i = currentHealth - 1; i >= 0; i--) 
        {
            health[(int)i].SetActive(true);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
