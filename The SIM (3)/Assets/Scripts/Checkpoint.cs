using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerg.SetActive(false);

            // Pindahkan posisi dan rotasi
            player.position = destination.position;
            player.rotation = destination.rotation;

            playerg.SetActive(true);
        }
    }
}
