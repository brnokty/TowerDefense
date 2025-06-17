using System;
using UnityEngine;

public class LosePanel : Panel
{
    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        // Additional initialization for FailPanel can be added here
    }

    public override void Appear(EventArgs eventArgs = null)
    {
        base.Appear(eventArgs);
        // Additional logic for appearing the FailPanel can be added here
    }

    public override void Disappear()
    {
        base.Disappear();
        // Additional logic for disappearing the FailPanel can be added here
    }
}
