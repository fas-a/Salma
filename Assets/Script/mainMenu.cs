using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject modal;

    public void PlayGame()
    {
        DisplayModal();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        GamePersistenceManager.instance.ContinueGame();
    }

    public void DisplayModal() {
        modal.SetActive(true);
    }

    public void HideModal()
    {
        modal.SetActive(false); // Nonaktifkan objek popup
    }

    public void EasyMode()
    {
        GamePersistenceManager.instance.StartNewGame(GamePersistenceManager.DifficultyLevel.Easy);
    }

    public void NormalMode()
    {
        GamePersistenceManager.instance.StartNewGame(GamePersistenceManager.DifficultyLevel.Medium);
    }

    public void HardMode()
    {
        GamePersistenceManager.instance.StartNewGame(GamePersistenceManager.DifficultyLevel.Hard);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Any initialization if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Any updates if needed
    }
}
