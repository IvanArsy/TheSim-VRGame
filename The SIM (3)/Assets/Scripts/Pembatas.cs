using UnityEngine;

public class Pembatas : MonoBehaviour
{
    public GameObject objectToHide;
    public GameObject objectToShow;
    private bool hasTriggered = false;

    void Start()
    {
        objectToHide.SetActive(true);
        objectToShow.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            objectToHide.SetActive(false);  // sembunyikan
            objectToShow.SetActive(true);   // tampilkan
            hasTriggered = true;            // tandai bahwa sudah pernah aktif
            gameObject.SetActive(false);
        }
    }
}