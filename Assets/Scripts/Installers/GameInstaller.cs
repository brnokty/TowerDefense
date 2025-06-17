using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject towerPrefab;

    public override void InstallBindings()
    {
        // EnemyData 
        var enemyData = Resources.Load<EnemyData>("ScriptableObjects/EnemyData");
        Container.Bind<EnemyData>()
            .FromScriptableObject(enemyData)
            .AsSingle();

        // EnemySpawner
        Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();

        // enemyPrefab 
        Container.Bind<GameObject>().WithId("EnemyPrefab").FromInstance(enemyPrefab);

        // WaveManager
        Container.BindInterfacesAndSelfTo<WaveManager>().AsSingle();
        //Projectile prefab
        Container.Bind<GameObject>()
            .WithId("ProjectilePrefab")
            .FromInstance(projectilePrefab);
        // Tower prefab
        Container.Bind<GameObject>()
            .WithId("TowerPrefab")
            .FromInstance(towerPrefab);

        // TowerManager
        Container.BindInterfacesAndSelfTo<TowerManager>().AsSingle();
        
        // UIManager
        Container.BindInterfacesAndSelfTo<UIManager>().AsSingle();

    }
}