using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActor : Actor
{
    PlayerController player;

    GameMaster gm;

    private void Awake()
    {
        player = GetComponent<PlayerController>();

        gm = FindObjectOfType<GameMaster>();
    }

    public override void ChangeHealthKnockback(float amount, Vector2 knockbackDirection)
    {
        ChangeHealth(amount);

        player.KnockBack(knockbackDirection.x);
    }
}
