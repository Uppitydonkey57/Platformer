using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public Vector2 parallaxSpeedMultiplier;

    Transform cameraTransform;
    Vector2 lastCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 deltaMovement = (Vector2)cameraTransform.position - lastCameraPosition;
        transform.position += (Vector3)new Vector2(deltaMovement.x * parallaxSpeedMultiplier.x, deltaMovement.y * parallaxSpeedMultiplier.y);
        lastCameraPosition = cameraTransform.position;
    }
}
