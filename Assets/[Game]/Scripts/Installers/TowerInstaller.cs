using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerInstaller : MonoInstaller
{
    
    [SerializeField] private GameObject projectilePrefab;
    
    public override void InstallBindings()
    {
        //Projectile prefab
        Container.Bind<GameObject>()
            .WithId("ProjectilePrefab")
            .FromInstance(projectilePrefab);
    }
}
