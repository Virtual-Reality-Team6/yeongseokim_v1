using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class XRControllerRight : MonoBehaviour
{
    private ActionBasedController controller;

    private void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    private void Update()
    {
        if (controller != null)
        {
            if (controller.selectAction.action.triggered)
            {
                Debug.Log("A 버튼이 눌렸습니다.");
            }

            if (controller.activateAction.action.triggered)
            {
                Debug.Log("B 버튼이 눌렸습니다.");
            }
        }
    }
}
