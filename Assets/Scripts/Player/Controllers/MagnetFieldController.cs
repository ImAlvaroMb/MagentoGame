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

    public void ChangeMagnetMode(MagnetismForceMode newMode)
    {
        _magnetismForceMode = newMode;
    }

    public void CalculateForcesToBeApplied()
    {
        Vector2 totalForceOnPlayer = Vector2.zero;

        for(int i = _nearbyObjects.Count -1; i >= 0; i--)
        {
            MagnetObject targetBody = _nearbyObjects[i];
            if(targetBody == null || targetBody.gameObject == gameObject)
            {
                _nearbyObjects.RemoveAt(i);
                continue;
            }

            Vector2 vectorPlayerToTarget = targetBody.transform.position - transform.position;
            float distance = vectorPlayerToTarget.magnitude;
            if (distance > maxEffectRange || distance <= 0.01f) continue;

            float aimFactor = GetAimFactor(vectorPlayerToTarget);
            if (aimFactor <= 0.01f) continue; //small threshold needs to be added

            MagnetismForceMode targetMagnetismMode = targetBody.MagnetismMode;

            float playerMagnetForce = CalculateForceMagnitude(baseMagenticForce, distance, aimFactor);
            playerMagnetForce = (_magnetismForceMode == MagnetismForceMode.ATTRACT) ? -1f * playerMagnetForce : playerMagnetForce; 

            switch(targetBody.ObjectType)
            {
                case MagnetObjectType.STATIC_WITH_FORCE: //calculate force that it applied to the platyer, add it or substract it to the the total force to apply to the player but not to the targetObject
                    float forceMagnitudeIn = CalculateForceMagnitude(targetBody.MaxMagneticForce, distance, aimFactor);
                    if(forceMagnitudeIn > 0.01f) // small threshold needs to be added
                    {
                        Vector2 directionIn = (targetBody.MagnetismMode == MagnetismForceMode.ATTRACT) ? vectorPlayerToTarget.normalized : -vectorPlayerToTarget.normalized;
                        totalForceOnPlayer += directionIn * forceMagnitudeIn;
                    }

                    break;

                case MagnetObjectType.NON_STATIC_WITH_FORCE: //calculate the force that the object needs to apply, in this case add it or substract it from the total force to paply to the player, and also apply it to the targetObject
                    float forceMagnitudeIn2 = CalculateForceMagnitude(targetBody.MaxMagneticForce, distance, aimFactor);
                    if(forceMagnitudeIn2 > 0.01f)
                    {
                        Vector2 directionIn2 = (targetBody.MagnetismMode == MagnetismForceMode.ATTRACT) ? vectorPlayerToTarget.normalized : -vectorPlayerToTarget.normalized;
                        totalForceOnPlayer += directionIn2 * forceMagnitudeIn2;
                    }
                    //targetBody.ApplyForceToObject(playerMagnetForce + forceMagnitudeIn2);

                    break;

                case MagnetObjectType.NON_STATIC_WITHOUT_FORCE://calculate force to apply yo this object only considering the player magnet effect and apply iyt to the taargetObject

                    break;
            }
        }
    }

    private float GetAimFactor(Vector2 vectorPlayerToTarget) //the closer the return value is to 1 the close it is aiming to the center of that object
    {
        float angleToTarget = Vector2.Angle(_aimDirection, vectorPlayerToTarget);
        if (angleToTarget > aimConeAngle) return 0;

        return 1f - (angleToTarget / aimConeAngle);
    }

    private float CalculateForceMagnitude(float baseStrenght, float distance, float aimFactor) // calculates final force using falloff and aim factor
    {
        float distanceFactor = distanceFalloff.Evaluate(distance);
        return baseStrenght * distanceFactor * aimFactor;
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
