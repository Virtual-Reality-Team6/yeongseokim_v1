using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class GameManager : Singleton<GameManager>
{
    //게임 매니저가 필요한 변수 세팅
    public Player player;

    public Button startButton;
    public Button ContinueButton;
    public bool hasPlaylog;
    public Button GalleryButton;
    public GameObject namePanel;
    public InputField newPlayerName;

    public string[] errandLists;
    public GameObject[] ErrandListObjects;
    public Text[] errandTexts;
    public Image checkFrontAImg;
    public Image checkFrontBImg;
    public Image checkFrontCImg;
    public Image checkFrontDImg;
    public Text listMamaText;

    public GameObject stampA;
    public GameObject stampB;
    public GameObject stampC;
    public GameObject stampD;
    public GameObject stampE;
    
    public float playTime;
    private float playTimeLimit = 10 * 60;
    public RectTransform timeBar;

    public bool IsTimerRunning;
    public GameObject TimeOutPanel;
    public Text TimeOutText;
    public Text menuTimeText;

    public GameObject startPanel;
    public GameObject gamePanel;
    public Text playTimeText;
    public Text playerCoinText;

    public GameObject endPanel;

    public Dictionary<ItemType, int> itemPool = new Dictionary<ItemType, int>();

    public int itemCount;

    public bool endShopping = false;

    public int selectionCount = 4;

    /// <summary>
    /// choice game
    /// </summary>
    public ChoiceManager choiceManager;
    public GameObject choicePanel;
    //public GameObject questPanel;
    public GameObject questcleaarimage;
    public GameObject questfailimage;
    private WaitForSeconds _UIDelay1 = new WaitForSeconds(5.0f);
    private WaitForSeconds _UIDelay2 = new WaitForSeconds(2.0f);
    public GameObject carroadButton;
    public GameObject sidewalkButton;
    public GameObject rightcolored;
    public GameObject wrongcolored;

    /// <summary>
    /// cross game
    /// </summary>
    /// 
    public GameObject crosswalkPanel;
    public GameObject clear;
    public GameObject fail;
    public GameObject oculus;
    public GameObject clearcross;
    public GameObject failcross;
    public GameObject clearimage;
    public GameObject failimage;
    public GameObject D;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;

    public GameObject subbox1;
    public GameObject subbox2;
    public GameObject subbox3;
    public GameObject subbox4;
    public GameObject subbox5;

    public float speed;
    float hAxis;
    float vAxis;
    Vector3 moveVec;
    private Vector3 targetPosition;

    private WaitForSeconds _UIDelay3 = new WaitForSeconds(2.0f);
    private float distance;
    bool timeup;
    bool success;

    public GameObject finishbutton;

    int flag_cross;
    int flag_choice;
    int coroutine;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);

        Debug.Log("awake");
    }

    void Start()
    {
        checkSaveFile();
        checkPlayLog();
        makeErrandList();

        choicePanel.SetActive(false);
        flag_cross = 0;
        flag_choice = 0;
        coroutine = 0;
    }

    void checkSaveFile()
    {
        // 세이브파일이 이미 있는지 체크
        if (File.Exists(SaveDataManager.instance.path + SaveDataManager.instance.filename))
        {
            hasPlaylog = true;
        }
    }

    void checkPlayLog()
    {
        if(hasPlaylog){
            Text startButtonText = startButton.GetComponentInChildren<Text>();
            startButtonText.text = "새 게임 시작";
            ContinueButton.gameObject.SetActive(true);
            GalleryButton.gameObject.SetActive(true);
        }
        
    }

    int[] generateRandomNumbers(){
        int[] selectedIndices = {-1, -1, -1, -1};
        bool[] boolMap = new bool[18];

        // 배열을 false로 초기화
        for (int i = 0; i < boolMap.Length; i++)
        {
            boolMap[i] = false;
        }
        
        for(int i=0; i<selectedIndices.Length; i++){
            int randomNum = Random.Range(0, errandLists.Length);
            if(!boolMap[randomNum]){
                selectedIndices[i] = randomNum;
                boolMap[randomNum] = true;
            }
            else i--;
        }
        return selectedIndices;
    }

    void makeErrandList()
    {
        itemCount = 0;
        int[] selectedIndices = generateRandomNumbers();

        for(int i=0; i<ErrandListObjects.Length; i++){
            GameObject AErrandList = ErrandListObjects[i];
            Image[] childImages = AErrandList.GetComponentsInChildren<Image>();

            int randomIndex = selectedIndices[i];
            Debug.Log(randomIndex);
            for (int j = 0; j < childImages.Length; j++){
                childImages[j].gameObject.SetActive(false);
            }
            childImages[randomIndex].gameObject.SetActive(true);

            int buyQuantity = Random.Range(1, 4);
            if(randomIndex >= 14) buyQuantity = 1;

            errandTexts[i].text = errandLists[randomIndex] + " " + buyQuantity.ToString() + " 개";

            itemPool.Add((ItemType)selectedIndices[i], buyQuantity);
            itemCount += buyQuantity;
            Debug.Log((ItemType)selectedIndices[i] + "," + buyQuantity);
        }
    }
    
    public void GameStart()
    {
        player.isMove = true;

        startPanel.SetActive(false);
        gamePanel.SetActive(true);

        IsTimerRunning = true;
    }

    public void NewGame()
    {
        namePanel.gameObject.SetActive(true); // 플레이어 닉네임 입력 UI를 활성화
    }

    public void NewGameStart()
    {
        SaveDataManager.instance.nowPlayer.name = newPlayerName.text;
        SaveDataManager.instance.nowPlayer.rewardScores = new int[SaveDataManager.rewardLength]; // 정수형은 0으로 초기화
        SaveDataManager.instance.SaveData();

        namePanel.SetActive(false);

        GameStart();
    }

    public void ContinueGame()
    {
        SaveDataManager.instance.LoadData(); // 데이터 로드
        GameStart();
    }

    public void GoGallery()
    {
        startPanel.SetActive(false);

        SaveDataManager.instance.LoadData(); // 데이터 로드
        SceneManager.LoadScene("Gallery");
    }

    public void GameEnd() // 플레이어가 집에 도착 또는 타임아웃
    {
        gamePanel.SetActive(false);
        TimeOutPanel.SetActive(false);
        endPanel.SetActive(true);

        GameObject.Find("End Panel").GetComponent<EndingScore>().ShowScore();

        playTime = 1; // update 함수에서 타임오버 창 뜨지 않도록
        IsTimerRunning = false;
    }

    public void TimeOutGameEnd()
    {
        TimeOutPanel.SetActive(false);
        GameEnd();
    }

    public void GoTitle() // 게임을 그만두고 타이틀 화면으로
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
        startPanel.SetActive(true);

        IsTimerRunning = false;

        // ***진행상황 초기화...***
        playTime = playTimeLimit;
    }

    public void QuitGame()
    {
        Application.Quit(); // 게임 종료
    }


    void Update()
    {
        if (playTime > 0F)
        {
            if (IsTimerRunning) playTime -= Time.deltaTime;
        }
        else
        {
            // time over
            // 타임오버 창 보여주기
            gamePanel.SetActive(false);
            TimeOutPanel.SetActive(true);
            // 게임 실패여야 하므로 진행상황과 관계없이 심부름 실패로 bool변수 업데이트
            Invoke("TimeOutGameEnd", 5f); // 5초 경과 후 엔딩 scene으로 이동

        }

        if (flag_cross == 1)
        {


            Player player = GameObject.Find("Player").GetComponent<Player>();

            hAxis = player.GetAxisRaw("Horizontal");
            vAxis = player.GetAxisRaw("Vertical");

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;

            if (one.activeSelf == false || two.activeSelf == false || three.activeSelf == false || four.activeSelf == false || five.activeSelf == false || six.activeSelf == false)
            {
                player.anim.SetBool("Run", moveVec == Vector3.zero);
            }


            player.transform.LookAt(player.transform.position + moveVec);

            if (one.activeSelf == false && two.activeSelf == false && three.activeSelf == false && four.activeSelf == false && five.activeSelf == false && six.activeSelf == false)
            {
                success = true;
                StopCoroutine("delay777");
                player.iscompletedStamp[1] = true;
                StartCoroutine(clear1());
                if (coroutine == 2)
                {
                    flag_cross = 0;
                    crosswalkPanel.SetActive(false);
                }


            }
        }
        if (flag_choice == 1)
        {
            player.iscompletedStamp[0] = true;
        }


    }

    void LateUpdate()
    {
        playerCoinText.text = string.Format("{0:n0}", player.coin);

        int min = (int)(playTime / 60);
        int second = (int)(playTime % 60);
        playTimeText.text = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);

        checkFrontAImg.color = new Color(1,1,1, player.iscompletedErrand[0] ? 1:0);
        checkFrontBImg.color = new Color(1,1,1, player.iscompletedErrand[1] ? 1:0); 
        checkFrontCImg.color = new Color(1,1,1, player.iscompletedErrand[2] ? 1:0); 
        checkFrontDImg.color = new Color(1,1,1, player.iscompletedErrand[3] ? 1:0);
        listMamaText.text = SaveDataManager.instance.nowPlayer.name + ", 5시까지 꼭 돌아와야 해!";


        timeBar.localScale = new Vector3((playTime)/playTimeLimit, 1, 1);

        stampA.SetActive(player.iscompletedStamp[0]);
        stampB.SetActive(player.iscompletedStamp[1]);
        stampC.SetActive(player.iscompletedStamp[2]);
        stampD.SetActive(player.iscompletedStamp[3]);
        stampE.SetActive(player.iscompletedStamp[4]);

        menuTimeText.text = playTimeText.text;
        TimeOutText.text = SaveDataManager.instance.nowPlayer.name + ", 어디 있니?\n이만 집으로 돌아오렴~";
    }

    public void ChoiceQuest(GameObject player)
    {

        choicePanel.SetActive(true);
        carroadButton.SetActive(true);
        sidewalkButton.SetActive(true);

        questcleaarimage.SetActive(false);
        questfailimage.SetActive(false);


        int s = choiceManager.Quest(player);



        if (rightcolored.activeSelf == false && wrongcolored.activeSelf == true)
        {
            StartCoroutine(delay1());
            flag_choice = 1;
            StartCoroutine(delay3());

            
        }
        else if (wrongcolored.activeSelf == false && rightcolored.activeSelf == true)
        {

            StartCoroutine(delay2());
            StartCoroutine(delay3());
        }
    }


    IEnumerator delay1()
    {
        questcleaarimage.SetActive(true);
        yield return _UIDelay1;
        questcleaarimage.SetActive(false);
    }

    IEnumerator delay2()
    {
        questfailimage.SetActive(true);
        yield return _UIDelay1;
        questfailimage.SetActive(false);
    }

    IEnumerator delay3()
    {
        yield return _UIDelay2;
        choicePanel.SetActive(false);
    }

    public int CrosswalkQuest(GameObject player)
    {


        crosswalkPanel.SetActive(true);

        oculus.SetActive(true);
        finishbutton.SetActive(true);

        finishbutton.SetActive(false);
        clearimage.SetActive(false);
        failimage.SetActive(false);

        timeup = false;
        success = false;

        clear.SetActive(false);
        fail.SetActive(false);

        D.SetActive(true);
        subbox1.SetActive(true);
        subbox2.SetActive(true);
        subbox3.SetActive(true);
        subbox4.SetActive(true);
        subbox5.SetActive(true);

        flag_cross = 1;

        StartCoroutine(delay777());

        return 0;

    }

    IEnumerator delay777()
    {
        yield return _UIDelay3;
        D.SetActive(false);

        yield return _UIDelay3;
        subbox1.SetActive(false);

        yield return _UIDelay3;
        subbox2.SetActive(false);

        yield return _UIDelay3;
        subbox3.SetActive(false);

        yield return _UIDelay3;
        subbox4.SetActive(false);

        yield return _UIDelay3;
        subbox5.SetActive(false);

        Debug.Log("times up!");
        timeup = true;

        yield return _UIDelay3;
        fail.SetActive(true);
        failcross.SetActive(false);
        yield return _UIDelay3;
        fail.SetActive(false);
        
        crosswalkPanel.SetActive(false);

    }

    IEnumerator clear1()
    {
        clear.SetActive(true);
        yield return _UIDelay3;
        clear.SetActive(false);
        coroutine = 2;
    }

}
