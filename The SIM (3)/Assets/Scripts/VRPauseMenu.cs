using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class VRPauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    private bool isPaused = false;
    public CarController carController;
    public InputActionReference menuButtonAction;


    void Start()
    {
        pauseCanvas.SetActive(false);
        menuButtonAction.action.Enable(); 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P ditekan");
            TogglePause();
        }
        if (menuButtonAction.action == null)
        {
            Debug.LogWarning("Menu Button Action belum di-assign!");
            return;
        }

        if (!menuButtonAction.action.enabled)
        {
            Debug.LogWarning("Menu Button Action belum aktif!");
            return;
        }

        if (menuButtonAction.action.WasPressedThisFrame())
        {
            Debug.Log("TOMBOL MENU DITEKAN");
            TogglePause();
        }
    }



    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseCanvas.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        carController.isPaused = isPaused;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        carController.isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToLobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
