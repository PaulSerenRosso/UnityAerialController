using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform ShootingPoint;
    
    [SerializeField] private float KillTimer;
    [SerializeField] private float AimRadius;
    [SerializeField] private float AimDistance;
    
    private Collider target;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        ShootProcess();
        CheckKillTarget();
    }

    private void ShootProcess()
    {
        RaycastHit hit;
        Physics.SphereCast(ShootingPoint.position, AimRadius, ShootingPoint.forward, out hit, AimDistance, 3);

        if (!hit.collider && !target)
            return;
        
        if (target)
        {
            if (!hit.collider || hit.distance > AimDistance)
            {
                Debug.Log("target lost");
                target = null;
                timer = 0f;
                return;
            }
            else if (hit.collider == target && hit.distance <= AimDistance)
            {
                timer += Time.deltaTime; 
                return;
            }
        }
        else if(hit.distance <= AimDistance)
        {
            Debug.Log("target found");
            target = hit.collider;
        } 
    }

    private void CheckKillTarget()
    {
        if (timer <= KillTimer) return;

        target.gameObject.GetComponent<EnemyManager>().Kill();    
        target = null;
        timer = 0f;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ShootingPoint.position, AimRadius);
        Gizmos.DrawWireSphere(ShootingPoint.position + ShootingPoint.forward * AimDistance, AimRadius);
    }
}

    