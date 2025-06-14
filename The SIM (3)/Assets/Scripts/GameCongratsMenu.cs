using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameCongratsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject congratsPanel;
    public Button backToLobbyButton;

    void Start()
    {
        congratsPanel.SetActive(true);
        backToLobbyButton.onClick.AddListener(OnBackToLobbyClicked);
    }

    void OnBackToLobbyClicked()
    {
        StartCoroutine(BackToLobbyRoutine());
    }

    IEnumerator BackToLobbyRoutine()
    {
        // Tutup panel dulu
        congratsPanel.SetActive(false);

        // Tunggu 1 frame agar fadeScreen muncul
        yield return null;

        // Panggil transisi dengan benar
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }
}

