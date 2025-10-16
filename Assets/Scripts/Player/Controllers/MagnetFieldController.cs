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
        Debug.Log(aimInput);

        if(isUsingController)
        {
            if(aimInput.sqrMagnitude > aimJoystickThreshold)
            {
                _aimDirection = aimInput.normalized;
            }
        } else
        {
            if(Camera.main != null)
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(aimInput);
                _aimDirection = ((Vector2) mouseWorldPosition - (Vector2)transform.position).normalized;
            }
        }

        Debug.DrawLine(transform.position, transform.position + (Vector3)_aimDirection * 5f);
    }
}
