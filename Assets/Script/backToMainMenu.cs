using UnityEngine;
using UnityEngine.SceneManagement;

public class backToMainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable() {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }
}
