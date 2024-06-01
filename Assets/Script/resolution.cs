using UnityEngine;
using UnityEngine.SceneManagement;

public class resolution : MonoBehaviour
{
    void OnEnable() {
        SceneManager.LoadScene("dapur", LoadSceneMode.Single);
    }
}
