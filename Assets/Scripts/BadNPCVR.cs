using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadNPCVR : MonoBehaviour
{
    public GameObject BadFirstPanel;
    public GameObject BadSecondPanel;
    public GameObject BadCorrectPanel;
    public GameObject BadWrongPanel;

    public GameObject playerObject;
    private Player player;
    public int interactionIndex;

    Player enterPlayer;
    public float rotationSpeed = 5f;

    Rigidbody rigid;
    CapsuleCollider capcol;
    public Transform target;
    UnityEngine.AI.NavMeshAgent nav;
    BoxCollider carparkCollider;
    
    public float stoppingDistance;

    void Awake()
    {
        HideAllPanel();
        player = playerObject.GetComponent<Player>();

        rigid = GetComponent<Rigidbody>();
        capcol = GetComponent<CapsuleCollider>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        carparkCollider = GameObject.FindWithTag("BadNPCArea").GetComponent<BoxCollider>();
        nav.isStopped = true;
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
        BadFirstPanel.SetActive(false);
        BadSecondPanel.SetActive(false);
        BadCorrectPanel.SetActive(false);
        BadWrongPanel.SetActive(false);
    }

    void Update()
    {
        if (carparkCollider.bounds.Contains(target.position))
        {
            // Player가 Carpark 영역에 들어온 경우 NPC를 쫓아갑니다.
            nav.SetDestination(target.position);
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= stoppingDistance)
            {
                // Player와 일정 거리(stoppingDistance) 안에 있는 경우 멈춥니다.
                nav.isStopped = true;
            }
            else
            {
                nav.isStopped = false;
            }
        }
        else
        {
            // Carpark 영역을 벗어난 경우 멈추도록 처리합니다.
            nav.isStopped = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            Debug.Log("플레이어와 부딪힘");
        }
    }
    
    public void StartBadInteraction()
    {
        HideAllPanel();
        BadFirstPanel.SetActive(true);
    }

    public void SecondBadInteraction()
    {
        HideAllPanel();
        BadSecondPanel.SetActive(true);
    }

    public void CorrectAnswerChoice()
    {
        HideAllPanel();
        BadCorrectPanel.SetActive(true);
        player.iscompletedStamp[interactionIndex] = true;
    }

    public void WrongAnswerChoice()
    {
        HideAllPanel();
        BadWrongPanel.SetActive(true);
    }
}
