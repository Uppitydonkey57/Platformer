using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitboxes : MonoBehaviour
{
    public Hitbox[] hitboxes;

    public Hitbox GetHitbox(string hitboxName)
    {
        foreach (Hitbox hitbox in hitboxes)
        {
            if (hitbox.name == hitboxName)
            {
                return hitbox;
            }
        }

        Debug.LogWarning($"The hitbox {hitboxName} does not exist");

        return null;
    }

    public bool DidHitboxCollide(string hitboxName)
    {
        Collider2D collider = CreateOverlap(hitboxName);

        if (collider != null)
        {
            return true;
        }

        return false;
    }

    Collider2D CreateOverlap(string hitboxName)
    {
        Hitbox hitbox = GetHitbox(hitboxName);

        if (hitbox != null)
        {
            switch (hitbox.shape)
            {
                case Hitbox.Shape.Square:
                    return Physics2D.OverlapBox((Vector2)transform.position + hitbox.offset, hitbox.size, 0, hitbox.collisionLayers);

                case Hitbox.Shape.Circle:
                    return Physics2D.OverlapCircle((Vector2)transform.position + hitbox.offset, hitbox.radius, hitbox.collisionLayers);

                case Hitbox.Shape.Point:
                    return Physics2D.OverlapPoint((Vector2)transform.position + hitbox.offset, hitbox.collisionLayers);
            }
        }
        return null;
    }

    void OnDrawGizmosSelected()
    {
        if (hitboxes != null)
        {
            foreach (Hitbox hitbox in hitboxes)
            {
                Gizmos.color = hitbox.color;

                switch (hitbox.shape)
                {
                    case Hitbox.Shape.Square:
                        Gizmos.DrawWireCube((Vector2)transform.position + hitbox.offset, hitbox.size);
                        break;

                    case Hitbox.Shape.Circle:
                        Gizmos.DrawWireSphere((Vector2)transform.position + hitbox.offset, hitbox.radius);
                        break;

                    case Hitbox.Shape.Point:
                        Gizmos.DrawSphere((Vector2)transform.position + hitbox.offset, 0.05f);
                        break;
                }
            }
        }
    }
}
