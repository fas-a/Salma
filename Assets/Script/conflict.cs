using UnityEngine;
using UnityEngine.SceneManagement;

public class conflict : MonoBehaviour
{
    void OnEnable() {
        SceneManager.LoadScene("resolution", LoadSceneMode.Single);
    }
}
