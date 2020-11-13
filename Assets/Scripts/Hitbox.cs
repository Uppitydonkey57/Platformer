using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Hitbox
{
    public string name;

    public enum Shape { Square, Circle, Point }

    public Shape shape;

    [Header("Make sure to make the color not transparent.")]
    public Color color = Color.red;

    public Vector2 offset;

    public Vector2 size = new Vector2(1f, 1f);

    public float radius = 1f;

    public LayerMask collisionLayers;
}
