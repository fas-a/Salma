using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreen : MonoBehaviour
{
    public string LevelName;
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(LevelName);
        }
    }
}
