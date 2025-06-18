using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;
    public float speed;
    public int health;
    public int reward; // Öldüğünde vereceği coin
    public EnemyType type;

    [Header("Support Tower Etkileri")]
    public float slowSensitivity = 1f;    // 1 = tam etkilenir, 0.5 = %50 etkilenir
    public float slowDuration = 1f;       // yavaşlatma süresi
}


public enum EnemyType
{
    Runner,
    Attacker
}