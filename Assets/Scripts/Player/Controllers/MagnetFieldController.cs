using UnityEngine;
using Enums;
using System.Collections.Generic;
public class MagnetFieldController : MonoBehaviour
{
    [SerializeField] private float baseMagenticForce = 100f;
    [SerializeField] private float maxEffectRange = 5f;
    [Tooltip("The offset angles from the center of the aim direction where the force still takes effect")]
    [SerializeField] private float aimConeAngle = 30f;
    [SerializeField] private float aimJoystickThreshold = 0.1f;

    [Tooltip("Defineso how the force decreases over the distance")]
    [SerializeField] private AnimationCurve distanceFalloff = AnimationCurve.Linear(0f,1f,5f,0f);

    [SerializeField] private Collider2D detectionCollider;

    private MagnetismForceMode _magnetismForceMode;

    private List<MagnetObject> _nearbyObjects = new List<MagnetObject>();
    private Vector2 _aimDirection = Vector2.right;

    private void Awake()
    {
        if (detectionCollider == null) Debug.LogError("Detection collider not assigned on" + this.name);
    }

    public void UpdateAimDirection(Vector2 aimInput, bool isUsingController)
    {
        Debug.Log(aimInput + "  "  + isUsingController);

        if(isUsingController)
        {
            if(aimInput.sqrMagnitude > aimJoystickThreshold)
            {
                _aimDirection = aimInput.normalized;

                Debug.DrawLine(transform.position, transform.position + (Vector3)_aimDirection * 15f, Color.red);
            }
        } else
        {
            if(Camera.main != null)
            {

                Plane aimingPlane = new Plane(Vector3.back, transform.position); //generate a plane on the player position aiming at the camera (z axis)
                Ray ray = Camera.main.ScreenPointToRay(aimInput);
                float hitDistance;
                if(aimingPlane.Raycast(ray, out hitDistance))
                {
                    Vector3 mouseWorldPosition = ray.GetPoint(hitDistance);
                    _aimDirection = ((Vector2)mouseWorldPosition - (Vector2)transform.position).normalized;

                    Debug.DrawLine(transform.position, transform.position + (Vector3)_aimDirection * 15f, Color.green);
                }
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MagnetObject magnetObject = collision.GetComponent<MagnetObject>();
        if(magnetObject != null && !_nearbyObjects.Contains(magnetObject))
        {
            _nearbyObjects.Add(magnetObject);
            Debug.Log($"Object: {magnetObject.name} is now added to nearbyObjects");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MagnetObject magnetObject = collision.GetComponent<MagnetObject>();
        if(magnetObject != null)
        {
            _nearbyObjects.Remove(magnetObject);
            Debug.Log($"Object: {magnetObject.name} is now deleted from nearbyObjects");
        }
    }
}
