using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardFireHandler : MonoBehaviour
{
   [SerializeField] private Enemy enemy;
   [SerializeField] private Transform handTransform;

   public void Fire()
   {
      if (enemy._behavior is AttackerBehavior attacker)
      {
         attacker.FireProjectileFrom(handTransform.position);
      }

   }
}
