using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerg;

    [Header("Audio")]
    public AudioSource whistleAudio; // Drag AudioSource berisi peluit di Inspector

    private bool sudahTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!sudahTriggered && other.CompareTag("Player"))
        {
            sudahTriggered = true;

            if (whistleAudio != null && whistleAudio.clip != null)
            {
                whistleAudio.Play();
                StartCoroutine(TeleportSetelahSuara(whistleAudio.clip.length));
            }
            else
            {
                // Kalau tak ada suara, langsung teleport
                TeleportPlayer();
            }
        }
    }

    IEnumerator TeleportSetelahSuara(float delay)
    {
        yield return new WaitForSeconds(delay);
        TeleportPlayer();
    }

    void TeleportPlayer()
    {
        playerg.SetActive(false);
        player.position = destination.position;
        player.rotation = destination.rotation;
        playerg.SetActive(true);
    }
}
