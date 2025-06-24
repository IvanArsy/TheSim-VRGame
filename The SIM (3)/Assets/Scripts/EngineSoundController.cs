using UnityEngine;

public class EngineSoundController : MonoBehaviour
{
    public Rigidbody carRigidbody;

    public AudioSource audioLow;
    public AudioSource audioHighAcc;
    public AudioSource audioHighDeacc;

    [Header("Pitch Settings")]
    public float minPitch = 1.0f;
    public float maxPitch = 2.0f;
    public float maxSpeed = 50f;

    [Header("Volume Settings")]
    public float lowIdleVolume = 0.5f;
    public float highVolume = 1f;
    public float transitionSpeed = 5f;

    private float lastSpeed;
    private float smoothAcceleration;

    void Start()
    {
        // Pastikan semua audio diatur dengan benar
        audioLow.loop = true;
        audioHighAcc.loop = true;
        audioHighDeacc.loop = true;

        audioLow.playOnAwake = false;
        audioHighAcc.playOnAwake = false;
        audioHighDeacc.playOnAwake = false;

        audioLow.volume = 1f;
        audioHighAcc.volume = 0f;
        audioHighDeacc.volume = 0f;

        audioLow.Play();
        audioHighAcc.Play();
        audioHighDeacc.Play();
    }

    void Update()
    {
        float speed = carRigidbody.linearVelocity.magnitude;
        float pitch = Mathf.Lerp(minPitch, maxPitch, speed / maxSpeed);
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Hitung akselerasi dengan smoothing
        float acceleration = (speed - lastSpeed) / Time.deltaTime;
        lastSpeed = Mathf.Lerp(lastSpeed, speed, Time.deltaTime * transitionSpeed);
        smoothAcceleration = Mathf.Lerp(smoothAcceleration, acceleration, Time.deltaTime * transitionSpeed);

        // Terapkan pitch
        audioLow.pitch = pitch;
        audioHighAcc.pitch = pitch;
        audioHighDeacc.pitch = pitch;

        // Target volume awal
        float targetLow = lowIdleVolume;
        float targetAcc = 0f;
        float targetDeacc = 0f;

        if (smoothAcceleration > 0.5f) // Akselerasi
        {
            targetLow = 0.2f;
            targetAcc = highVolume;
        }
        else if (smoothAcceleration < -0.5f) // Deselerasi
        {
            targetLow = 0.2f;
            targetDeacc = highVolume;
        }

        // Lerp volume agar transisi halus
        audioLow.volume = Mathf.Lerp(audioLow.volume, targetLow, Time.deltaTime * transitionSpeed);
        audioHighAcc.volume = Mathf.Lerp(audioHighAcc.volume, targetAcc, Time.deltaTime * transitionSpeed);
        audioHighDeacc.volume = Mathf.Lerp(audioHighDeacc.volume, targetDeacc, Time.deltaTime * transitionSpeed);
    }
}
