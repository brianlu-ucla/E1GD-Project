using System;
using UnityEngine;

public class Endpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                int remainingJumps = pm.MaxJumps - pm.JumpCount;
                int remainingDashes = pm.MaxDashes - pm.DashCount;
                int bonus = (remainingJumps + remainingDashes) * 100;
                ScoreManager.instance.AddPoints(bonus);
            }
            ScoreManager.instance.SwitchLevel();
        }
    }

}
