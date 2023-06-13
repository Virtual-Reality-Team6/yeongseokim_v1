using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class HelloNPCXR : MonoBehaviour
{
    public GameObject uiCanvas;

    private XRController controller;
    private bool isUIVisible = false;

    void Start()
    {
        controller = GetComponent<XRController>();
        uiCanvas.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        // 트리거 버튼이 눌렸을 때 처리
        if (controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
        {
            // 현재 레이캐스트로 가리키는 객체 가져오기
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, controller.transform.forward, out hit))
            {
                if (hit.collider.CompareTag("HelloNPC"))
                {
                    // HelloNPC 오브젝트를 가리키고 트리거 버튼을 눌렀을 때 UI 토글
                    ToggleUI();
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