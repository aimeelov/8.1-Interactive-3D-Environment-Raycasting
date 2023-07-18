using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class InteractionSystem : MonoBehaviour
{
    public GameObject focusedObject;
    public GameObject pickupSlot;
    public bool holding;
    public TextMeshProUGUI UseText;

    private Rigidbody playerRigidbody;
    private Rigidbody itemRigidbody;
    private GameObject mailObject; // Reference to the mail game object


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // If player is holding, return
        if (holding)
            return;

        // Compute player's forward direction
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        // Create raycast hit variable to store our results
        RaycastHit hit;

        // Ray originating from camera
        Ray ray = new Ray(transform.position, fwd);

        // Conduct raycast
        if (Physics.Raycast(ray, out hit))
        {
            focusedObject = hit.collider.gameObject;
            if (focusedObject.CompareTag("Interactable") || focusedObject.CompareTag("Mailbox") || focusedObject.CompareTag("Lamp") || focusedObject.CompareTag("TV") || focusedObject.CompareTag("Cat"))
            {
                // Display specific text for the interactable object
                string interactText = GetInteractText(focusedObject);
                UseText.text = interactText;
                UseText.gameObject.SetActive(true);
            }
            else
            {
                // Hide the use text if not focused on an interactable object
                UseText.gameObject.SetActive(false);
            }
        }
        else
        {
            focusedObject = null;
            // Hide the use text if not focused on any object
            UseText.gameObject.SetActive(false);
        }
    }

    private string GetInteractText(GameObject obj)
    {
        // Customize the text based on the specific interactable object
        if (obj.CompareTag("Lamp"))
        {
            return "Press E to switch the light on and off";
        }
        else if (obj.CompareTag("TV"))
        {
            return "Press R to turn the TV on and off";
        }
        else if (obj.CompareTag("Cat"))
        {
            return "Greet the cat by walking up to it";
        }
        else if (obj.CompareTag("Interactable"))
        {
            return "Press Q to pickup and drop item";
        }
        else if (obj.CompareTag("Mailbox"))
        {
            return "Press Q to get mail from mailbox";
        }
        else
        {
            return ""; // Return empty string if no specific text is defined
        }
    }

    public void OnInteract()
    {

        if (holding)
        {
            // Check if the focused object is a basket
            if (focusedObject.CompareTag("Basket"))
            {
                // Drop the item into the basket
                focusedObject.GetComponent<BasketScript>().ReceiveItem(focusedObject);
            }
            else
            {
                // Drop the item normally
                focusedObject.transform.parent = null;
                focusedObject.GetComponent<Rigidbody>().isKinematic = false;
            }

            // Reset holding state
            holding = false;
        }
        else if (focusedObject.CompareTag("Interactable"))
        {
            // Regular interactable object, pick it up
            focusedObject.transform.parent = pickupSlot.transform;
            focusedObject.transform.position = pickupSlot.transform.position;
            Rigidbody focusedRigidbody = focusedObject.GetComponent<Rigidbody>();
            if (focusedRigidbody != null)
            {
                focusedRigidbody.isKinematic = true;
            }
            holding = true;
        }
        else if (focusedObject.CompareTag("Mailbox"))
        {
            // Check if there is mail inside the mailbox
            GameObject mail = GameObject.Find("mail"); // Assuming the mail object has the name "Mail"
            if (mail != null && mail.CompareTag("Interactable"))
            {
                // Check if the mail is already being held
                if (holding)
                {
                    // Drop the mail
                    mail.transform.parent = null;
                    Rigidbody mailRigidbody = mail.GetComponent<Rigidbody>();
                    if (mailRigidbody != null)
                    {
                        mailRigidbody.isKinematic = false;
                    }
                    holding = false;
                }
                else
                {
                    // Pick up the mail
                    mail.transform.parent = pickupSlot.transform;
                    mail.transform.position = pickupSlot.transform.position;
                    focusedObject = mail; // Update the focusedObject to be the mail
                    Rigidbody mailRigidbody = mail.GetComponent<Rigidbody>();
                    if (mailRigidbody != null)
                    {
                        mailRigidbody.isKinematic = true;
                    }
                    holding = true;
                }
            }
        }

    }
}
