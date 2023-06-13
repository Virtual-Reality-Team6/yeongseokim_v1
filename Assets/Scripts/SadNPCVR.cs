using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadNPCVR : MonoBehaviour
{
    public GameObject SadFirstPanel;
    public GameObject SadSecondPanel;
    public GameObject SadCorrectPanel;
    public GameObject SadWrongPanel;

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
        SadFirstPanel.SetActive(false);
        SadSecondPanel.SetActive(false);
        SadCorrectPanel.SetActive(false);
        SadWrongPanel.SetActive(false);
    }
    
    public void StartSadInteraction()
    {
        HideAllPanel();
        SadFirstPanel.SetActive(true);
    }

    public void SecondSadInteraction()
    {
        HideAllPanel();
        SadSecondPanel.SetActive(true);
    }

    public void CorrectAnswerChoice()
    {
        HideAllPanel();
        SadCorrectPanel.SetActive(true);
        player.iscompletedStamp[interactionIndex] = true;
    }

    public void WrongAnswerChoice()
    {
        HideAllPanel();
        SadWrongPanel.SetActive(true);
    }

}
