using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float speed;
    public int health;
    public EnemyType type;
}

public enum EnemyType
{
    Runner,
    Attacker
}