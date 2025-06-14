using UnityEngine;
using System.Collections;

public class CheckpointTest : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Vector3 teleportPosition = new Vector3(3.1f, 0.61f, -27.08f); // Posisi tetap
    public Quaternion teleportRotation = Quaternion.Euler(0, 0, 0);      // Rotasi tetap
    public GameObject collisionObject;
    public float resetDelay = 1f;
    public ParticleSystem teleportEffect;
    public AudioClip teleportSound;

    [Header("Ground Settings")]
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer;
    public float additionalStabilizationForce = 10f;

    private Rigidbody vehicleRigidbody;
    private bool isResetting = false;

    private void Start()
    {
        vehicleRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionObject != null && collision.gameObject == collisionObject && !isResetting)
        {
            isResetting = true;
            Invoke("ResetVehicle", resetDelay);
        }
    }

    private void ResetVehicle()
    {
        // Matikan physics sementara
        vehicleRigidbody.isKinematic = true;

        // Pindahkan kendaraan dengan rotasi yang stabil
        transform.position = GetGroundedPosition();
        transform.rotation = GetStableRotation();

        // Efek dan suara
        PlayTeleportEffects();

        // Hidupkan kembali physics
        vehicleRigidbody.isKinematic = false;

        // Reset velocity dan tambahkan stabilisasi
        StartCoroutine(StabilizeVehicle());

        isResetting = false;
    }

    private Vector3 GetGroundedPosition()
    {
        RaycastHit hit;
        Vector3 targetPos = teleportPosition;

        // Cek ground dengan sphere cast untuk hasil lebih akurat
        if (Physics.SphereCast(teleportPosition + Vector3.up * 2f, 0.5f, Vector3.down, out hit, 3f, groundLayer))
        {
            targetPos = hit.point + Vector3.up * groundCheckDistance;
        }

        return targetPos;
    }

    private Quaternion GetStableRotation()
    {
        return teleportRotation;
    }

    private IEnumerator StabilizeVehicle()
    {
        yield return new WaitForFixedUpdate();

        // Reset velocity
        vehicleRigidbody.linearVelocity = Vector3.zero;
        vehicleRigidbody.angularVelocity = Vector3.zero;

        // Tambahkan force ke bawah untuk stabilisasi
        vehicleRigidbody.AddForce(Vector3.down * additionalStabilizationForce, ForceMode.Impulse);

        // Pastikan mobil sejajar dengan ground
        yield return new WaitForSeconds(0.1f);
        AlignWithGround();
    }

    private void AlignWithGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f, groundLayer))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Euler(targetRotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetRotation.eulerAngles.z);
        }
    }

    private void PlayTeleportEffects()
    {
        if (teleportEffect != null)
        {
            teleportEffect.transform.position = transform.position;
            teleportEffect.Play();
        }

        if (teleportSound != null)
        {
            AudioSource.PlayClipAtPoint(teleportSound, transform.position);
        }
    }

    // Visual debugging (optional)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(teleportPosition, 0.2f);
        Gizmos.DrawLine(teleportPosition + Vector3.up * 2f, teleportPosition + Vector3.down * 3f);
    }
}
