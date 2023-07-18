using System.Collections;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public float runRadius = 0.4f; // Radius of the circle the cat will run along
    public float runSpeed = 1f; // How fast the cat runs
    private bool isMoving = false; // If the cat is currently moving
    private Animator animator; // Animator component reference
    private float angle = 0; // Current angle on the circle
    private float yRotation = 0; // Current y rotation of the cat
    private Vector3 initialPosition; // Initial position of the cat


    void Start()
    {
        SphereCollider sc = gameObject.AddComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = 7f; // Set this to the distance the player has to approach before the cat starts running
        animator = GetComponent<Animator>(); // Get the Animator component
        initialPosition = transform.position; // Store the initial position of the cat
                                              
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player (replace "Player" with the player's tag)
        if (other.gameObject.CompareTag("Player") && !isMoving)
        {
            StartRunning();
        }
    }

    public void StartRunning()
    {
        Debug.Log("StartRunning called.");
        isMoving = true;
        animator.SetTrigger("startMovement");
        StartCoroutine(RunInCircle());
    }

    public void StopRunning()
    {
        if (!isMoving)
            return; // The cat is already idle

        Debug.Log("StopRunning called.");
        isMoving = false;
        animator.ResetTrigger("startMovement");
    }

    IEnumerator RunInCircle()
    {
        Vector3 startPos = transform.position; // Store the starting position
        Vector3 endPos = CalculatePositionOnCircle(angle); // Calculate the end position along the circle

        while (isMoving)
        {
            // Calculate the direction vector towards the end position
            Vector3 direction = endPos - startPos;
            direction.y = 0;

            // Calculate the desired rotation based on the direction of movement
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);

            // Update the y rotation smoothly towards the desired rotation
            yRotation = Mathf.LerpAngle(yRotation, desiredRotation.eulerAngles.y, Time.deltaTime * runSpeed);

            // Apply the rotation to the cat
            transform.rotation = Quaternion.Euler(0, yRotation, 0);

            // Move the cat towards the end position
            transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * runSpeed);

            // If the cat has reached the end position, calculate the next end position along the circle
            if (Vector3.Distance(transform.position, endPos) < 0.01f)
            {
                angle -= Time.deltaTime * runSpeed; // Increment the angle for the next frame (to move counterclockwise)
                startPos = endPos; // Set the start position for the next movement
                endPos = CalculatePositionOnCircle(angle); // Calculate the new end position along the circle
            }

            yield return null;
        }

        Debug.Log("Exiting RunInCircle coroutine.");

        animator.ResetTrigger("startMovement"); // Reset the startMovement trigger
        animator.SetTrigger("stopMovement"); // Set the stopMovement trigger to indicate the end of the Run animation
    }

    Vector3 CalculatePositionOnCircle(float angle)
    {
        Vector3 initialPositionOnCircle = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);
        Vector3 offset = Quaternion.Euler(0, angle * Mathf.Rad2Deg, 0) * Vector3.forward * runRadius;
        return initialPositionOnCircle + offset;
    }

}

