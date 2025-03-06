using System;
using UnityEngine;

public class Endpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            ScoreManager.instance.SwitchLevel();
    }

}
