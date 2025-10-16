using UnityEngine;
using Enums;
public class MagnetObject : MonoBehaviour
{
    [SerializeField] private float maxMagneticForce = 50f;
    [SerializeField] private float attractionMultiplier = 1.0f;
    [SerializeField] private float repulsionMultiplier = 1.0f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
}
