using UnityEngine;
using System.Collections;

public class Pembatas : MonoBehaviour
{
    public GameObject objectToHide;
    public GameObject objectToShow;
    public GameObject popup;
    public AudioSource audioSource;
    private bool hasTriggered = false;

    void Start()
    {
        objectToHide.SetActive(true);
        objectToShow.SetActive(false);
        popup.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            objectToHide.SetActive(false);
            objectToShow.SetActive(true);
            popup.SetActive(true);
            hasTriggered = true;

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
                StartCoroutine(DisableAfterSound(audioSource.clip.length)); // Tunda disable
            }
            else
            {
                gameObject.SetActive(false); // Tetap disable jika tak ada audio
            }
        }
    }

    private IEnumerator DisableAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
