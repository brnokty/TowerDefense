using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Game Data")] [SerializeField] private WaveData[] waves;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private TowerData[] towerDatas;


    public override void InstallBindings()
    {
        // // EnemyData 
        // var enemyData = Resources.Load<EnemyData>("ScriptableObjects/Enemies/EnemyData");
        // Container.Bind<EnemyData>()
        //     .FromScriptableObject(enemyData)
        //     .AsSingle();

        // EnemySpawner
        Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();

        // UIManager
        Container.BindInterfacesAndSelfTo<UIManager>().AsSingle();

        // Wave datası
        Container.Bind<WaveData[]>().FromInstance(waves).AsSingle();


        // WaveManager
        Container.BindInterfacesAndSelfTo<WaveManager>().AsSingle();
        //Projectile prefab
        Container.Bind<GameObject>()
            .WithId("ProjectilePrefab")
            .FromInstance(projectilePrefab);

        // Tower prefab
        Container.Bind<TowerData[]>().FromInstance(towerDatas).AsSingle();

        // TowerManager
        Container.BindInterfacesAndSelfTo<TowerManager>().AsSingle();


        //CoinManager
        Container.BindInterfacesAndSelfTo<CoinManager>().AsSingle();

        // InGamePanel
        Container.Bind<InGamePanel>().FromComponentInHierarchy().AsSingle();

        //win panel
        Container.Bind<WinPanel>().FromComponentInHierarchy().AsSingle();

        //lose panel
        Container.Bind<LosePanel>().FromComponentInHierarchy().AsSingle();
    }
}