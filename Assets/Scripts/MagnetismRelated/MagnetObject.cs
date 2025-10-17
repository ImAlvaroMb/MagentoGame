using UnityEngine;
using Enums;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class MagnetObject : MonoBehaviour
{
    public float MaxMagneticForce => maxMagneticForce;
    public float AttractionMultiplier => attractionMultiplier;
    public float RepulsionMultiplier => repulsionMultiplier;
    public bool IsStatic => isStatic;
    public MagnetismForceMode MagnetismMode => magnetismMode;


    [SerializeField] private float maxMagneticForce = 50f;
    [SerializeField] private float attractionMultiplier = 1.0f;
    [SerializeField] private float repulsionMultiplier = 1.0f;
    [Tooltip("If this object can be moved by the force of the players magnet or not")]
    [SerializeField] private bool isStatic = false;
    [Tooltip("What type of force this object is going to apply towards the players magnet")]
    [SerializeField] private MagnetismForceMode magnetismMode;
    
    private Rigidbody2D _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (isStatic) _rb.bodyType = RigidbodyType2D.Kinematic;  
    }
}
