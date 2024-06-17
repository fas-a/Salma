using UnityEngine;
using UnityEngine.SceneManagement;

public class nextSceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable() {
        SceneManager.LoadScene("dapur", LoadSceneMode.Single);
    }
}
