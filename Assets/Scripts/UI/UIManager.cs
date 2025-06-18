using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class UIManager : IInitializable
{
    [HideInInspector] public InGamePanel _inGamePanel;
    
    public void Initialize()
    {
        _inGamePanel = GameObject.Find("InGamePanel").GetComponent<InGamePanel>();
    }
}