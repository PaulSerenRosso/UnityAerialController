using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public UIManager uiManager;

    [SerializeField] private int health;
    [SerializeField] private float invulnerabilityTimer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forceScale = 3750f;

    private bool invulnerable;
    private void OnCollisionEnter(Collision other)
    {
        if (invulnerable) return;
        health--;
        rb.AddForce((-rb.velocity).normalized * forceScale, ForceMode.Impulse);
        uiManager.UpdateHealth(health);
        invulnerable = true;
        Invoke("ResetInvulnerability", invulnerabilityTimer);
        GetComponent<AirplaneController>().Rotate();
    }

    private void ResetInvulnerability()
    {
        invulnerable = false;
    }

    public bool IsDead()
    {
        return health == 0;
    }
}
