using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class XRControllerCommon : MonoBehaviour
{

    private ActionBasedController controller;
    private bool isUIVisible = false;

    private void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    private void Update()
    {
        if (controller != null)
        {
            Debug.Log("트리거가 눌렸다.");
            if (controller.activateAction.action.ReadValue<float>() > 0.5f)
            {
                
                RaycastHit hit;
                if (Physics.Raycast(controller.transform.position, controller.transform.forward, out hit))
                {
                    if (hit.collider.CompareTag("HelloNPC"))
                    {
                        Debug.Log("HelloNPC 인식");
                        HelloNPCVR helloNPCVR = hit.collider.GetComponent<HelloNPCVR>();
                        helloNPCVR.StartHelloInteraction();
                        Debug.Log(helloNPCVR);
                    }
                }
            }
        }
    }
}
