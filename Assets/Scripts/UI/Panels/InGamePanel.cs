using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _towerCountText;

    private int _wave = 1;
    private int _availableTowers = 3;
    
    protected override void Start()
    {
        base.Start();
        UpdateWaveText();
        UpdateTowerText();
    }
    
    public void SetWave(int wave)
    {
        _wave = wave;
        UpdateWaveText();
    }

    public void SetAvailableTowers(int count)
    {
        _availableTowers = count;
        UpdateTowerText();
    }

    private void UpdateWaveText()
    {
        _waveText.text = $"Wave: {_wave}";
    }

    private void UpdateTowerText()
    {
        _towerCountText.text = $"Towers: {_availableTowers}";
    }
}