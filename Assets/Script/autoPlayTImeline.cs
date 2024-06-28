using UnityEngine;
using UnityEngine.Playables;

public class autoPlayTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector;

    void Start()
    {
        if (playableDirector != null)
        {
            playableDirector.Play();
        }
        else
        {
            Debug.LogError("PlayableDirector is not assigned.");
        }
    }
}
