using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;
    private GamePersistenceManager gamePersistenceManager;

    public void pauseGame(){
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    public void unpauseGame(){
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void saveGame() {
        GamePersistenceManager.instance.SaveGame();
    }

    public void exit()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
