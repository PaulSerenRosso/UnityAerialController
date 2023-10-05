using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public UIManager uiManager;
    
    [SerializeField] private Transform ShootingPoint;
    [SerializeField] private float KillTimer;
    [SerializeField] private float AimRadius;
    [SerializeField] private float AimDistance;
    
    private Collider target;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        uiManager.UpdateLockTimer(KillTimer - timer);
        ShootProcess();
        CheckKillTarget();
    }

    private void ShootProcess()
    {
        RaycastHit hit;
        Physics.SphereCast(ShootingPoint.position, AimRadius, ShootingPoint.forward, out hit, AimDistance);
        
        if (!hit.collider && !target)
            return;
        
        if (target)
        {
            if (!hit.collider.CompareTag("Enemy") || hit.distance > AimDistance)
            {
                target = null;
                return;
            }
            else if (hit.collider == target && hit.distance <= AimDistance)
            {
                timer += Time.deltaTime; 
                return;
            }
        }
        else if(hit.distance <= AimDistance && hit.collider.CompareTag("Enemy"))
        {
            uiManager.StartLockTimer(true);
            target = hit.collider;
        } 
        ResetTimer();
    }

    private void CheckKillTarget()
    {
        if (timer <= KillTimer) return;

        target.gameObject.GetComponent<EnemyManager>().Kill();    
        target = null;
        ResetTimer();
    }
    
    private void ResetTimer()
    {
        if (!target)
        {
            timer = 0f;
            uiManager.StartLockTimer(false);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ShootingPoint.position, AimRadius);
        Gizmos.DrawWireSphere(ShootingPoint.position + ShootingPoint.forward * AimDistance, AimRadius);
    }
}

    