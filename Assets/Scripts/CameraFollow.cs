using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraFollow: MonoBehaviour
{
    public Transform target; // Target object
    public float distance = 1.3f; // Distance from the target
    public float height = 1.5f; // Height of the camera
    public float mouseSensitivity = 1.8f; // Mouse sensitivity for rotation

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private void OnEnable()
    {
        this.gameObject.GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if(BallController.Singleton != null && target == null)
        {
            target = BallController.Singleton.transform;
            BallController.Singleton.camTransform = transform;
        }

        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        if (target != null)
        {
            // If the target is not assigned, exit the function
            if (!target)
                return;

            // Calculate the rotation based on mouse input
            rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Clamp rotationY to prevent flipping
            rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f);

            // Calculate the rotation quaternion
            Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);

            // Calculate the position offset
            Vector3 offset = new Vector3(0, height, -distance);

            // Rotate the offset based on the current rotation
            offset = rotation * offset;

            // Set the camera's position and make it look at the target
            transform.position = target.position + offset;
            transform.LookAt(target.position);
        }
    }
}
