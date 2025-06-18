using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI _waveText;

    private int _wave = 1;
    private int _availableTowers = 3;
    
    protected override void Start()
    {
        base.Start();
        UpdateWaveText();
    }
    
    public void SetWave(int wave)
    {
        _wave = wave;
        UpdateWaveText();
    }



    private void UpdateWaveText()
    {
        _waveText.text = $"Wave: {_wave}";
    }
}