using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string currentMapName;
    public float speed;
    public float jumpUp;
    float hAxis;
    float vAxis;

    bool shiftDown;
    bool spaceDown;
    bool interactionDown;
    bool memoDown;
    bool stampDown;
    bool menuDown;

    public int coin;
    public int score;

    bool isJump;
    bool isShop;
    public bool isInteraction;

    bool seeMemo;
    bool seeStamp;
    bool seeMenu = false;

    public bool[] iscompletedErrand;
    public bool[] iscompletedStamp;

    Vector3 moveVec;

    Rigidbody rigid;
    public Animator anim;

    GameObject nearObject;

    public RectTransform memoUIGroup;
    public RectTransform stampUIGroup;
    public RectTransform menuUIGroup;

    public Quaternion rot;
    public SceneLoader sceneLoader;

    public bool isMove = true;
    public bool hasKey;

    /// <summary>
    /// quest game
    /// </summary>
    public GameManager gameManager;
    public ChoiceManager choiceManager;
    public GameObject ChoiceStart, ChoiceStart1, ChoiceStart2, ChoiceStart3;
    public GameObject CrossStart, CrossStart1, CrossStart2;
    private float distance0, distance1, distance2, distance3, distance4, distance5, distance6;
    private int choiceDone, crossDone;

    public GameObject choicePanel;
    public GameObject carroadButton;
    public GameObject sidewalkButton;
    public GameObject rightcolored;
    public GameObject wrongcolored;

    public GameObject crosswalkPanel;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    private Vector3 targetPosition;


    void Start()
    {
        choiceDone = 0;
        crossDone = 0;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        //PlayerPrefs.SetInt("MaxScore", 112500);
        Debug.Log("다시 깨어나는가");
    }
    
    void Update()
    {
        if (!isMove) return; 


        GetInput();
        Move();
        Turn();
        Jump();
        Interaction();
        Memo();
        Stamp();
        Menu();

        //choice game
        distance0 = Vector3.Distance(transform.position, ChoiceStart.transform.position);
        distance1 = Vector3.Distance(transform.position, ChoiceStart1.transform.position);
        distance2 = Vector3.Distance(transform.position, ChoiceStart2.transform.position);
        distance3 = Vector3.Distance(transform.position, ChoiceStart3.transform.position);

        if (choiceDone == 0)
        {
            if (distance0 <= 10f || distance1 <= 2f || distance2 <= 2f || distance3 <= 5f)
            {

                choicePanel.SetActive(true);
                carroadButton.SetActive(true);
                sidewalkButton.SetActive(true);


                if (rightcolored.activeSelf == false || wrongcolored.activeSelf == false)
                {
                    Debug.Log("clicked");
                    choiceDone = Choice();

                }

            }
        }


        //crosswalk game
        distance4 = Vector3.Distance(transform.position, CrossStart.transform.position);
        distance5 = Vector3.Distance(transform.position, CrossStart1.transform.position);
        distance6 = Vector3.Distance(transform.position, CrossStart2.transform.position);

        if (crossDone == 0)
        {
            if (distance4 <= 5f || distance5 <= 5f || distance6 <= 5f)
            {
                crosswalkPanel.SetActive(true);
                crossDone = Crosswalk();
            }
        }


    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        shiftDown = Input.GetButton("Run");
        spaceDown = Input.GetButton("Jump");
        interactionDown = Input.GetButtonDown("Interaction");
        memoDown = Input.GetButtonDown("Memo");
        stampDown = Input.GetButtonDown("Stamp");
        menuDown = Input.GetButtonDown("Menu");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position +=
             rot * moveVec * speed * (shiftDown ? 2f : 1f) * Time.deltaTime;

        anim.SetBool("Walk", moveVec != Vector3.zero);
        anim.SetBool("Run", shiftDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (spaceDown && !isJump && !isInteraction){
            rigid.AddForce(Vector3.up * jumpUp,  ForceMode.Impulse);
            anim.SetBool("Jump", true);
            anim.SetTrigger("Jump");
            isJump = true;
        }
    }

    void Interaction()
    {
        if(interactionDown && nearObject != null){
            if(nearObject.tag == "HelloNPC"){
                HelloNPC hellonNPC = nearObject.GetComponent<HelloNPC>();
                hellonNPC.Enter(this);
                isInteraction = true;
            }
            if(nearObject.tag == "BadNPC"){
                BadNPC badNPC = nearObject.GetComponent<BadNPC>();
                badNPC.Enter(this);
                isInteraction = true;
            }
            if(nearObject.tag == "SadNPC"){
                SadNPC sadNPC = nearObject.GetComponent<SadNPC>();
                sadNPC.Enter(this);
                isInteraction = true;
            }
        }
    }

    void Memo()
    {
        if(memoDown){
            if(seeMemo) {
                memoUIGroup.anchoredPosition = Vector3.down * 1000;
                seeMemo = false;
            }
            else {
                memoUIGroup.anchoredPosition = Vector3.zero;
                seeMemo = true;
            }
        }
    }

    public void Stamp()
    {
        if(stampDown){
            if(seeStamp) {
                stampUIGroup.anchoredPosition = Vector3.down * 1000;
                seeStamp = false;
            }
            else {
                stampUIGroup.anchoredPosition = new Vector3(718f, 0f, 0f);
                seeStamp = true;
            }
        }
    }

    public void Menu()
    {
        if(menuDown){
            if(seeMenu) {
                menuUIGroup.anchoredPosition = Vector3.left * 1500;
                GameManager.Instance.IsTimerRunning = true;
                seeMenu = false;

            }
            else {
                menuUIGroup.anchoredPosition = new Vector3(0f, 0f, 0f);
                GameManager.Instance.IsTimerRunning = false;
                seeMenu = true;
            }
        }
    }

    public void MenuClose()
    {
        menuUIGroup.anchoredPosition = Vector3.left * 1500;
        GameManager.Instance.IsTimerRunning = true;
        seeMenu = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor"){
            isJump = false;
        }
        if(collision.gameObject.CompareTag("Portal")){
            GameObject sceneLoaderObject = GameObject.Find("Scene Loader");
            sceneLoader = sceneLoaderObject.GetComponent<SceneLoader>();
            if(sceneLoader == null){
                Debug.Log("sceneLoader is null");
            }
            else{
                sceneLoader.LoadScene();
            }
        }

        if (collision.gameObject.CompareTag("Home"))
        {
            GameManager.Instance.GameEnd();
        }
    }

    void OnTriggerStay(Collider other){
        if(other.tag == "HelloNPC" || other.tag == "BadNPC" || other.tag == "SadNPC")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "HelloNPC"){
            HelloNPC hellonNPC = nearObject.GetComponent<HelloNPC>();
            hellonNPC.Exit();
            isInteraction = false;
            nearObject = null;
        }
        if(other.tag == "BadNPC"){
            BadNPC badNPC = nearObject.GetComponent<BadNPC>();
            badNPC.Exit();
            isInteraction = false;
            nearObject = null;
        }
        if(other.tag == "SadNPC"){
            SadNPC sadNPC = nearObject.GetComponent<SadNPC>();
            sadNPC.Exit();
            isInteraction = false;
            nearObject = null;
        }
    }

    int Choice()
    {
        Debug.Log("this!");
        gameManager.ChoiceQuest(this.gameObject);

        return 1;
    }

    int Crosswalk()
    {
        Debug.Log("this!");
        int n = gameManager.CrosswalkQuest(this.gameObject);

        return 1;
    }

    public float GetAxisRaw(string axisName)
    {
        return Input.GetAxisRaw(axisName);
    }

}
