using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayGame() {
        GamePersistenceManager.instance.NewGame();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadGame() {
        GamePersistenceManager.instance.LoadGame();
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
