using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class HelloNPCXR : MonoBehaviour
{
    public GameObject uiCanvas;

    private ActionBasedController controller;
    private bool isUIVisible = false;

    void Awake()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("업데이트 수행");
        if (controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
        {
            Debug.Log("트리거는 눌림");
        }
    }

    private void ToggleUI()
    {
        isUIVisible = !isUIVisible;
        uiCanvas.SetActive(isUIVisible);
    }
}