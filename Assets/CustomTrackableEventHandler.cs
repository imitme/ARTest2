using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{
    public bool videoContinuous = true;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        TurnSwitchComponent<TV>(true);

        if (videoContinuous)
        {
            var videoPlayers = GetComponentsInChildren<VideoPlayer>(true);
            foreach (var vp in videoPlayers)
                vp.Play();
        }
        else
            TurnSwitchComponent<VideoPlayer>(true);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        TurnSwitchComponent<TV>(false);
        if (videoContinuous)
        {
            var videoPlayers = GetComponentsInChildren<VideoPlayer>(true);
            foreach (var vp in videoPlayers)
                vp.Pause();
        }
        else
            TurnSwitchComponent<VideoPlayer>(false);
    }

    private void TurnSwitchComponent<T>(bool turnOn) where T : Behaviour
    {
        var components = GetComponentsInChildren<T>(true);
        foreach (var c in components)
            c.enabled = turnOn;
    }
}