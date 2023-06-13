using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class HelloNPCXR : MonoBehaviour
{
    public GameObject uiCanvas;

    private ActionBasedController controller;
    private bool isUIVisible = false;

    private void Start()
    {
        controller = GetComponent<ActionBasedController>();
        uiCanvas.SetActive(false);
    }

    private void Update()
    {
        if (controller != null)
        {
            // 트리거 버튼이 눌렸을 때 처리
            if (controller.activateAction.action.ReadValue<float>() > 0.5f)
            {
                // 현재 레이캐스트로 가리키는 객체 가져오기
                RaycastHit hit;
                if (Physics.Raycast(controller.transform.position, controller.transform.forward, out hit))
                {
                    if (hit.collider.CompareTag("HelloNPC"))
                    {
                        HelloNPCVR helloNPCVR = hit.collider.GetComponent<HelloNPCVR>();
                        helloNPCVR.StartHelloInteraction();
                    }
                }
            }
        }
    }

    private void ToggleUI()
    {
        isUIVisible = !isUIVisible;
        uiCanvas.SetActive(isUIVisible);
    }
}
