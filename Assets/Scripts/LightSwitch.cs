using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public bool IsOn;

    private void Start()
    {
        IsOn = false;
        this.GetComponent<Light>().enabled = IsOn;
    }

    private void Update()
    {
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IsOn = !IsOn;
                this.GetComponent<Light>().enabled = IsOn;
            }
        }
    }

   // public void lightOn()
   // {
   //     if (!IsOn)
   //     {
   //         if (Input.GetKey(KeyCode.E))
   //             this.GetComponent<Light>().enabled = true;
   //     }
  //     }

  //       public void lightOff()
  //      {
  //          if (IsOn)
  //          {
  //              if (Input.GetKey(KeyCode.E))
  //                  this.GetComponent<Light>().enabled = false;
  //          }
  //     }

}

