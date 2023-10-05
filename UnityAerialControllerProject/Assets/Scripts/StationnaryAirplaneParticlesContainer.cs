using System;
using System.Collections;
using System.Collections.Generic;
using HelperPSR.MonoLoopFunctions;
using UnityEngine;

public class StationnaryAirplaneParticlesContainer : MonoBehaviour, IUpdatable
{
   [SerializeField] private ParticleSystem particleSystem;
   [SerializeField] private Transform pivotPoint;
   [SerializeField] private float offset;

   private void Start()
   {
      UpdateManager.Register(this);
   }

   public void OnUpdate()
   {
      particleSystem.transform.position = pivotPoint.position + offset*Vector3.down;
   }
}
