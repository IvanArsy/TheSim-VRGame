using System.Collections;
using UnityEngine;
using TMPro;

public class ParkirParalel : MonoBehaviour
{
    [Header("Player References")]
    public Transform player;
    public Transform destination;
    public GameObject playerg;
    public Rigidbody mobilRb;

    [Header("UI Elements")]
    public TextMeshProUGUI countdownText;
    public GameObject countdownPanel;

    [Header("Settings")]
    public float waktuDiam = 5f;
    public float velocityThreshold = 0.2f;

    [Header("Audio")]
    public AudioSource countdownAudioSource; // Tambahkan untuk suara countdown

    private bool rodaBelakangKiriMasuk = false;
    private bool rodaDepanKiriMasuk = false;
    private float timer = 0f;
    private bool teleportSudahDilakukan = false;
    private bool suaraSudahDimainkan = false; // Untuk mencegah suara dimainkan berulang

    void Start()
    {
        if (countdownPanel != null)
            countdownPanel.SetActive(false);

        if (countdownAudioSource != null)
            countdownAudioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RodaBelakangKiri"))
        {
            rodaBelakangKiriMasuk = true;
            Debug.Log("Roda belakang kiri masuk area parkir");
        }
        else if (other.CompareTag("RodaDepanKiri"))
        {
            rodaDepanKiriMasuk = true;
            Debug.Log("Roda depan kiri masuk area parkir");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RodaBelakangKiri"))
        {
            rodaBelakangKiriMasuk = false;
            Debug.Log("Roda belakang kiri keluar area parkir");
        }
        else if (other.CompareTag("RodaDepanKiri"))
        {
            rodaDepanKiriMasuk = false;
            Debug.Log("Roda depan kiri keluar area parkir");
        }
    }

    void Update()
    {
        if (rodaBelakangKiriMasuk && rodaDepanKiriMasuk && !teleportSudahDilakukan)
        {
            if (mobilRb.linearVelocity.magnitude < velocityThreshold)
            {
                timer += Time.deltaTime;

                if (countdownPanel != null && !countdownPanel.activeSelf)
                    countdownPanel.SetActive(true);

                // Mainkan suara hanya sekali
                if (!suaraSudahDimainkan && countdownAudioSource != null && countdownAudioSource.clip != null)
                {
                    countdownAudioSource.Play();
                    suaraSudahDimainkan = true;
                }

                float sisaWaktu = Mathf.Clamp(waktuDiam - timer, 0, waktuDiam);
                if (countdownText != null)
                    countdownText.text = $"Berhasil parkir!\nTeleport dalam {sisaWaktu:F1} detik...";

                if (timer >= waktuDiam)
                {
                    StartCoroutine(TeleportPlayer());
                }
            }
            else
            {
                ResetCountdown();
            }
        }
        else
        {
            ResetCountdown();
        }
    }

    void ResetCountdown()
    {
        timer = 0f;
        suaraSudahDimainkan = false; // Reset agar bisa diputar lagi jika masuk ulang
        if (countdownPanel != null)
            countdownPanel.SetActive(false);
    }

    IEnumerator TeleportPlayer()
    {
        teleportSudahDilakukan = true;

        if (countdownText != null)
            countdownText.text = "Memindahkan...";

        yield return new WaitForSeconds(0.5f);

        if (countdownPanel != null)
            countdownPanel.SetActive(false);

        playerg.SetActive(false);
        yield return null;
        player.position = destination.position;
        player.rotation = destination.rotation;
        playerg.SetActive(true);
    }
}
