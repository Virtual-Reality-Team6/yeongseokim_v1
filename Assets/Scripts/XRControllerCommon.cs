using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class XRControllerCommon : MonoBehaviour
{

    private ActionBasedController controller;
    public bool hasCoin;

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
                    }
                    if (hit.collider.CompareTag("BadNPC"))
                    {
                        Debug.Log("BadNPC 인식");
                        BadNPCVR badNPCVR = hit.collider.GetComponent<BadNPCVR>();
                        badNPCVR.StartBadInteraction();
                    }
                    if (hit.collider.CompareTag("SadNPC"))
                    {
                        Debug.Log("SadNPC 인식");
                        SadNPCVR sadNPCVR = hit.collider.GetComponent<SadNPCVR>();
                        if(hasCoin) sadNPCVR.StartSadInteraction();
                        else sadNPCVR.WrongAnswerChoice();
                    }
                }
            }

            if (controller.selectAction.action.triggered)
            {
                // A 버튼 동작 처리
                Debug.Log("A 버튼이 눌렸습니다.");
            }

            // B 버튼을 눌렀을 때 처리
            if (controller.activateAction.action.triggered)
            {
                // B 버튼 동작 처리
                Debug.Log("B 버튼이 눌렸습니다.");
            }
        }
    }
}
