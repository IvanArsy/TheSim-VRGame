using UnityEngine;

public class SuaraMobil : MonoBehaviour
{
    public AudioSource audioSource;
    public Rigidbody rb;
    public float kecepatanMinimum = 0.1f;

    void Update()
    {
        // Cek kecepatan mobil
        float speed = rb.linearVelocity.magnitude;

        if (speed > kecepatanMinimum)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Pause(); // atau Stop() jika ingin ulang dari awal
        }
    }
}
