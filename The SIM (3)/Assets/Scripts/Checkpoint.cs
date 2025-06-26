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
        if (other.CompareTag("Player"))
        {
            if (whistleAudio != null && whistleAudio.clip != null)
            {
                whistleAudio.Play();
                StartCoroutine(TeleportSetelahSuara(whistleAudio.clip.length));
            }
            else
            {
                TeleportPlayer();
            }
        }
    }


    IEnumerator TeleportSetelahSuara(float delay)
    {
        Debug.Log("Coroutine started. Will wait: " + delay);
        yield return new WaitForSeconds(delay);
        Debug.Log("Delay done. Calling TeleportPlayer...");
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
