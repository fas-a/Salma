using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class timelineManager : MonoBehaviour
{
    public PlayableDirector timeline;

    void Start()
    {
        // Subscribe to the signal receiver
        timeline.stopped += OnTimelineStopped;
    }

    // Method to skip to a specific time
    public void SkipToTime(double time)
    {
        timeline.time = time;
        timeline.Play();
    }

    // Method to handle signal from Signal Emitter
    public void HandleSignalFromTimeline(string signalName)
    {
        // Example: If the signal name matches a predefined action
        if (signalName == "NextScene")
        {
            // Move to the next scene time (example time)
            SkipToTime(15.0); // Adjust time as needed
        }
        // Add more conditions or actions based on signal names
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        // Handle when the timeline stops (optional)
    }
}
