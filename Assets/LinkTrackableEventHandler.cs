using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LinkTrackableEventHandler : DefaultTrackableEventHandler
{
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        var connectComponents = GetComponentsInChildren<Connect>(true);
        // Enable connect':
        foreach (var component in connectComponents)
            component.enabled = true;
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        var connectComponents = GetComponentsInChildren<Connect>(true);
        // Disable connect':
        foreach (var component in connectComponents)
            component.enabled = false;
    }
}