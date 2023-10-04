using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float invulnerabilityTimer;
    [SerializeField] private GameObject ui;

    private bool invulnerable;
    private void OnCollisionEnter(Collision other)
    {
        health--;
        ui.GetComponent<UIManager>().UpdateHealth(health);
        invulnerable = true;
        Invoke("ResetInvulnerability", invulnerabilityTimer);
    }

    private void ResetInvulnerability()
    {
        invulnerable = false;
    }
}
