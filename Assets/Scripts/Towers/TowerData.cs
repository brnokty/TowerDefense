using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Data/TowerData")]
public class TowerData : ScriptableObject
{
    public int towerIndex;
    public string towerName;
    public Sprite towerSprite;
    public GameObject towerPrefab;
    public TowerType towerType;
    public int towerCost;
    public float towerRange;

    [Header("Combat Tower")]
    public float fireRate;
    public int damage;

    [Header("Support Tower")]
    public float healAmount;
    public float supportInterval;
    public float slowDuration;
    public float slowAmount;
}

public enum TowerType
{
    Shooter,
    Support
}