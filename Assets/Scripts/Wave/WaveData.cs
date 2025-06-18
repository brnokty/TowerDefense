using UnityEngine;

[CreateAssetMenu(menuName = "Data/WaveData")]
public class WaveData : ScriptableObject
{
    public EnemyData[] enemyTypes;
    public int[] enemyCounts;
    public float spawnDelay = 0.7f;

    public int TotalEnemies => enemyCounts.Length > 0 ? System.Linq.Enumerable.Sum(enemyCounts) : 0;
}