using UnityEngine;
// using UnityEngine.SceneManagement;

public class useAlat : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with ");
        // SceneManager.LoadScene(transform.name);
    }
}
