using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloNPCVR : MonoBehaviour
{
    public GameObject HelloFirstPanel;
    public GameObject HelloSecondPanel;
    public GameObject HelloCorrectPanel;
    public GameObject HelloWrongPanel;

    public GameObject playerObject;
    private Player player;
    public int interactionIndex;

    Rigidbody rigid;
    public float rotationSpeed = 5f;

    void Awake()
    {
        HideAllPanel();
        player = playerObject.GetComponent<Player>();
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }

    public void HideAllPanel()
    {
        HelloFirstPanel.SetActive(false);
        HelloSecondPanel.SetActive(false);
        HelloCorrectPanel.SetActive(false);
        HelloWrongPanel.SetActive(false);
    }
    
    public void StartHelloInteraction()
    {
        HideAllPanel();
        HelloFirstPanel.SetActive(true);
    }

    public void SecondHelloInteraction()
    {
        HideAllPanel();
        HelloSecondPanel.SetActive(true);
    }

    public void CorrectAnswerChoice()
    {
        HideAllPanel();
        HelloCorrectPanel.SetActive(true);
        player.iscompletedStamp[interactionIndex] = true;
    }

    public void WrongAnswerChoice()
    {
        HideAllPanel();
        HelloWrongPanel.SetActive(true);
    }

}
