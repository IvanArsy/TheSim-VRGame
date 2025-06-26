using UnityEngine;
using UnityEngine.InputSystem;

public class CarAudioManager : MonoBehaviour
{
    [Header("Audio Clip")]
    public AudioClip backgroundMusic; // Drag file audio kamu ke sini

    [Header("Input")]
    public InputActionReference toggleMusicButton; // Binding ke buttonB kanan

    private AudioSource dynamicAudioSource;
    private bool isPlaying = false;

    void Start()
    {
        // Buat AudioSource secara dinamis di runtime
        dynamicAudioSource = gameObject.AddComponent<AudioSource>();
        dynamicAudioSource.clip = backgroundMusic;
        dynamicAudioSource.loop = true;
        dynamicAudioSource.playOnAwake = false;

        toggleMusicButton.action.performed += ToggleMusic;
        toggleMusicButton.action.Enable();
    }

    private void ToggleMusic(InputAction.CallbackContext context)
    {
        if (isPlaying)
        {
            dynamicAudioSource.Pause();
        }
        else
        {
            dynamicAudioSource.Play();
        }

        isPlaying = !isPlaying;
    }

    private void OnDestroy()
    {
        toggleMusicButton.action.performed -= ToggleMusic;
    }
}
