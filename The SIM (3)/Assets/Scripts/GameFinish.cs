using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource finishAudioSource; // Suara saat player mencapai akhir

    private bool sudahTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!sudahTriggered && other.CompareTag("Player"))
        {
            sudahTriggered = true;

            if (finishAudioSource != null && finishAudioSource.clip != null)
            {
                finishAudioSource.Play();
                StartCoroutine(PindahSceneSetelahSuara(finishAudioSource.clip.length));
            }
            else
            {
                SceneManager.LoadScene(3); // Jika tidak ada audio, langsung pindah scene
            }
        }
    }

    private IEnumerator PindahSceneSetelahSuara(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(3);
    }
}
