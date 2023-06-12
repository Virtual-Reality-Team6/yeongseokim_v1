using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class crosswalk : MonoBehaviour
{
    public GameObject Player;

    public GameObject subbox1;
    public GameObject subbox2;
    public GameObject subbox3;
    public GameObject subbox4;
    public GameObject subbox5;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;

    public GameObject clear;
    public GameObject fail;


    private WaitForSeconds _UIDelay1 = new WaitForSeconds(2.0f);
    private float distance;
    bool timeup;
    bool success;

    // Start is called before the first frame update
    void Start()
    {
        timeup = false;
        success = false;

        clear.SetActive(false);
        fail.SetActive(false);

        subbox1.SetActive(true);
        subbox2.SetActive(true);
        subbox3.SetActive(true);
        subbox4.SetActive(true);
        subbox5.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(delay1());


    }

    // Update is called once per frame
    void Update()
    {

        if (one.activeSelf == false && two.activeSelf == false && three.activeSelf == false && four.activeSelf == false && five.activeSelf == false && six.activeSelf == false)
        {
            Debug.Log("great job!");
            success = true;
            StopCoroutine("delay1");
            StartCoroutine(clear1());
            StopAllCoroutines();

            Change();


        }

    }

    public void Change()
    {
        SceneManager.LoadScene("SimpleTown_DemoScene");
    }

    IEnumerator delay1()
    {
        yield return _UIDelay1;
        subbox1.SetActive(false);

        yield return _UIDelay1;
        subbox2.SetActive(false);

        yield return _UIDelay1;
        subbox3.SetActive(false);

        yield return _UIDelay1;
        subbox4.SetActive(false);

        yield return _UIDelay1;
        subbox5.SetActive(false);

        Debug.Log("times up!");
        timeup = true;

        fail.SetActive(true);
        yield return _UIDelay1;
        fail.SetActive(false);

        Change();

    }

    IEnumerator clear1()
    {
        clear.SetActive(true);
        yield return _UIDelay1;
        //clear.SetActive(false);
    }


}
