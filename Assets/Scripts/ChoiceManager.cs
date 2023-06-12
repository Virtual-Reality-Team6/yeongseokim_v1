using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public Player player;

    //public GameObject choicePanel;
    public GameObject goodPanel;
    public GameObject wrongPanel;

    public GameObject carroadButton;
    public GameObject sidewalkButton;

    private WaitForSeconds _UIDelay1 = new WaitForSeconds(0.5f);

    private WaitForSeconds _UIDelayLong = new WaitForSeconds(5.0f);

    private bool isQuestComplete = false;

    //public clickCarroad clickcar;
    //public clickSidewalk clickside;

    public GameObject rightcolored;
    public GameObject wrongcolored;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int Quest(GameObject player)
    {

       
            if (rightcolored.activeSelf == false && wrongcolored.activeSelf == true)
            {
                Choice_wrong();
            }
            else if (wrongcolored.activeSelf == false && rightcolored.activeSelf == true)
            {
                Choice_wrong();
            }
        

        return 0;

    }


    IEnumerator delayLong()
    {

        yield return _UIDelayLong;

    }         


    public void Choice_clear()
    {
        Debug.Log("right choice!");

    }

    public void Choice_wrong()
    {
        Debug.Log("Wrong choice!");

    }


    public IEnumerator delay1()
    {
        goodPanel.SetActive(true);
        yield return _UIDelay1;
        goodPanel.SetActive(false);
    }

    public IEnumerator delay2()
    {
        wrongPanel.SetActive(true);
        yield return _UIDelay1;
        wrongPanel.SetActive(false);
    }


}
