using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;
    public float speed;
    public int health;
    public int reward; // Öldüğünde vereceği coin
    public int baseDamage; //basee değince verceği hasar
    public EnemyType type;
    
    [Header("Attacker Özellikleri")]
    public float attackRange = 1.5f; 
    public float attackRate = 1f; 

    [Header("Support Tower Etkileri")]
    public float slowSensitivity = 1f;    // 1 = tam etkilenir, 0.5 = %50 etkilenir
}


public enum EnemyType
{
    Runner,
    Attacker
}