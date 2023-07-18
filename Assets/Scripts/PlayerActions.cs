using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using static UnityEngine.InputSystem.Controls.AxisControl;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class PlayerActions : MonoBehaviour
{
    public TextMeshPro UseText; // Drag your TextMeshProUGUI element to this field in Inspector
    public Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask UseLayers;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            if (hit.collider.gameObject.CompareTag("Lamp"))
            {
                UseText.text = "Press E to switch light on and off";
            }
            else if (hit.collider.gameObject.CompareTag("TV"))
            {
                UseText.text = "Press R to turn TV on and off";
            }
            else if (hit.collider.gameObject.CompareTag("Cat"))
            {
                UseText.text = "Greet Cat by walking up to it";
            }
            //else if (hit.collider.gameObject.CompareTag("Interactable"))
            //{
            //    UseText.text = "Press Q to pickup and drop item";
            //}
            else if (hit.collider.gameObject.CompareTag("Mailbox"))
            {
                UseText.text = "Press Q to get mail from mailbox";
            }
            else
            {
                UseText.text = "";
            }

            UseText.gameObject.SetActive(true);
            UseText.transform.localScale = Vector3.one;
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}





