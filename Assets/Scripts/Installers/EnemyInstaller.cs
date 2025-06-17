using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // EnemyData 
        var enemyData = Resources.Load<EnemyData>("ScriptableObjects/EnemyData");
        Container.Bind<EnemyData>()
            .FromScriptableObject(enemyData)
            .AsSingle();
    }
}