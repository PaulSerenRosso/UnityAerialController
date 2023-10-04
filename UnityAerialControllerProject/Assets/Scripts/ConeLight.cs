using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeLight : Cone
{

   [SerializeField] private float ratioHeightLight;
   protected override void Start()
   {
      base.Start();
      meshRenderer.material.SetFloat("_ConeHeight", ratioHeightLight*height);
   }
}
