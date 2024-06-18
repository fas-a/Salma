using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class skipManager : MonoBehaviour
{
    [SerializeField]
    PlayableDirector playableDirector;

    public void play(float time) {
        playableDirector.time = time;   
    }

    public void next(float time) {
        playableDirector.time += time; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
