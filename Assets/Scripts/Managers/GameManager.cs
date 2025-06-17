using UnityEngine;
using Zenject;

public class GameManager : IInitializable
{
    public void Initialize()
    {
        Debug.Log("✅ GameManager Inject edildi ve initialize edildi!");
    }
}