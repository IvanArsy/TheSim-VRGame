using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    // Nama tag yang digunakan oleh player (pastikan GameObject player punya tag ini)
    public string playerTag = "Player";


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            SceneManager.LoadScene(2);
        }
    }
}
