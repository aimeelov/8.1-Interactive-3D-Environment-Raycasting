using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPCamera : MonoBehaviour
{
    // The camera attached to the player
    public Camera playerCamera;

    // container variables for the mouse delta values each frame
    public float mouseSensitivity = 2f;
    public float deltaX;
    public float deltaY;

    // Container variables for the player's rotation around the X and Y axis
    public float xRot; // rotation around the x-axis in degrees
    public float yRot; // rotation around the y-axis in degrees

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main; // set player camera
        Cursor.visible = false; // hide the cursor

    }

    // Update is called once per frame
    void Update()
    {

        // Adjust mouse deltas by sensitivy 
        deltaX *= mouseSensitivity;
        deltaY *= mouseSensitivity;

        // keep track of players x and y rotations
        yRot += deltaX;
        xRot -= deltaY;

        // Keep the player's x rotation clamped to [-90, 90] degrees
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        // rotate the camera around the x axis
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    // OnCameraLook event handler
    public void OnCameraLook(InputValue value)
    {
        // Reading the mouse deltas as a vetor 2 (delta x is the x component and delta Y is the y component of the vector)
        Vector2 inputVector = value.Get<Vector2>();
        deltaX = inputVector.x;
        deltaY = inputVector.y;
    }
}

