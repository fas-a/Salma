using UnityEngine;

public class pauseManager : MonoBehaviour
{
    private bool isPaused = false;

    public void pauseGame(){
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void unpauseGame(){
        Time.timeScale = 1f;
        isPaused = false;
    }
}
