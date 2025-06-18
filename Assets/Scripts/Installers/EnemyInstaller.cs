using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    
    [SerializeField] private EnemyData[] enemyDatas;
    public override void InstallBindings()
    {
        // Tower prefab
        Container.Bind<EnemyData[]>().FromInstance(enemyDatas).AsSingle();
    }
}