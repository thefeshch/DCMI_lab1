using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool clipCamera;
    [SerializeField] float rotationSpeed = 120.0f;
    [SerializeField] float elevationSpeed = 120.0f;

    [SerializeField] float elevationMinLimit = -20f;
    [SerializeField] float elevationMaxLimit = 80f;
    [SerializeField] float distance = 5.0f;
    [SerializeField] float distanceMin = .5f;
    [SerializeField] float distanceMax = 15f;

    float rotationAroundTarget = 0.0f;
    float elevationToTarget = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationAroundTarget = angles.y;
        elevationToTarget = angles.x;

        if (target)
        {
            // Take the current distance from the camera to the target
            float currentDistance =
                (transform.position - target.position).magnitude;

            // Clamp it to our required minimum/maximum
            distance = Mathf.Clamp(
                currentDistance, distanceMin, distanceMax);
        }
    }

    // Every frame, after all Update functions are called, update the
    // camera position and rotation
    //
    // We do this in LateUpdate so that if the object we're tracking has
    // its position changed in the Update method, the camera will be
    // correctly positioned, because LateUpdate is always run afterward.
    void LateUpdate()
    {
        if (target)
        {
            rotationAroundTarget +=
                Input.GetAxis("Mouse X")
                    * rotationSpeed * distance * 0.02f;

            elevationToTarget -=
                Input.GetAxis("Mouse Y") * elevationSpeed * 0.02f;

            elevationToTarget = ClampAngle(
                elevationToTarget,
                elevationMinLimit,
                elevationMaxLimit
            );

            // Compute the rotation based on these two angles
            Quaternion rotation = Quaternion.Euler(
                elevationToTarget,
                rotationAroundTarget,
                0
            );

            // Update the distance based on mouse movement
            distance = distance - Input.GetAxis("Mouse ScrollWheel") * 5;

            // And limit it to the minimum and maximum
            distance = Mathf.Clamp(distance, distanceMin, distanceMax);

            // Figure out a position that's "distance" units away
            // from the target in the reverse direction to where
            // we're looking
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;

            if (clipCamera)
            {
                RaycastHit hitInfo;

                var ray =
                    new Ray(target.position, position - target.position);
                var hit = Physics.Raycast(ray, out hitInfo, distance);


                if (hit)
                {
                    Debug.Log("hit is true");
                    position = hitInfo.point;
                }
            }
            transform.position = position;
            Debug.LogFormat("orbiting:{0}", distance);
        }

    }

    // Clamps an angle between "min" and "max," wrapping it if it's less
    // than 360 degrees or higher than 360 degrees.
    public static float ClampAngle(float angle, float min, float max)
    {

        // Wrap the angle at -360 and 360
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;

        // Clamp this wrapped angle
        return Mathf.Clamp(angle, min, max);
    }
}
