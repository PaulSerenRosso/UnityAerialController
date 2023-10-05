using System;
using System.Collections;
using System.Collections.Generic;
using HelperPSR.MonoLoopFunctions;
using UnityEngine;
using UnityEngine.ProBuilder;

public class SpeedParticleContainer : MonoBehaviour
{
    [SerializeField] private float maxParticleCount;

    [SerializeField] private float minParticleCount;

    [SerializeField] private float minParticleSpeed;

    [SerializeField] private float maxParticleSpeed;

    [SerializeField] private ParticleSystem particleSystem;

    [SerializeField]
    private float testSpeedNormalized;
    public void DeactivateParticle()
    {
        particleSystem.Stop();
    
    }

    private void Start()
    {
        ActivateParticle();
    }

    public void ActivateParticle()
    {
        particleSystem.Play();
     
    }

    private void Update()
    {
        UpdateParticle(testSpeedNormalized);
    }

    public void UpdateParticle(float speedNormalized)
    {
        var particleSystemEmission = particleSystem.emission;
        particleSystemEmission.rateOverTime = new ParticleSystem.MinMaxCurve(minParticleCount+speedNormalized*maxParticleCount);
        var particleSystemMain = particleSystem.main;
        particleSystemMain.startSpeed =
            new ParticleSystem.MinMaxCurve(minParticleSpeed + speedNormalized * maxParticleSpeed);
    }


   
}
