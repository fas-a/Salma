using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayGame() {
        GamePersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(3);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadGame() {
        GamePersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync(3);
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
