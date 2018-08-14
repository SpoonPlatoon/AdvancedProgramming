using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{
    public bool hideCursor = false;

    [Header("Collision")]
    public bool cameraCollision = false;
    public float camRadius = 1f;

    public float rayDistance = 1000f;
    public LayerMask ignoreLayer;

    public Vector3 offset = new Vector3(0,1,0);
    private Vector3 originalOffset;

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        if(hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //calculate offset of camera at start
        originalOffset = transform.position - target.position;
        //Ray distance is as long as the magnitude of offset
        rayDistance = originalOffset.magnitude;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        
        transform.SetParent(null);

    }
    void FixedUpdate()
    {
        if(target)
        {
            if(cameraCollision)
            {
                //create ray that goes backwards from target
                Ray camRay = new Ray(target.position, -transform.forward);
                RaycastHit hit;
                if(Physics.SphereCast(camRay, camRadius, out hit, rayDistance, ~ignoreLayer, QueryTriggerInteraction.Ignore))
                {
                    distance = hit.distance;
                    //return - exit the function
                    return;
                }
            }

            //reset distance if not hitting
            distance = originalOffset.magnitude;
        }
    }
    public void Look(float mouseX, float mouseY)
    {
        x += mouseX * xSpeed * Time.deltaTime;
        y -= mouseY * ySpeed * Time.deltaTime;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;
    }

    void LateUpdate()
    {
        if (target)
        {
            Vector3 localOffset = transform.TransformDirection(offset);
            transform.position = (target.position + localOffset) + -transform.forward * distance;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, camRadius);
    }
}