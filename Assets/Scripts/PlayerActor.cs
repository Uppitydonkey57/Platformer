using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActor : Actor
{
    PlayerController player;

    GameMaster gm;

    HealthUI healthUI;
    

    private void Awake()
    {
        player = GetComponent<PlayerController>();

        gm = FindObjectOfType<GameMaster>();

        healthUI = FindObjectOfType<HealthUI>();
    }

    public override void ChangeHealthKnockback(float amount, Vector2 knockbackDirection)
    {
        ChangeHealth(amount);

        player.KnockBack(knockbackDirection.x);

        healthUI.UpdateHealth(health);
    }
}
