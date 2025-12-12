using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) // Gets triggered when player enters the water/barrier
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restarts the scene
        }
    }
}
