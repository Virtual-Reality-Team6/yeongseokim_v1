using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    public GameObject player;
    public GameObject thisroad1;
    public GameObject thisroad2;
    //public GameObject alert;
    private float distance1;
    private float distance2;

    private WaitForSeconds _UIDelay1 = new WaitForSeconds(2.0f);

    void Start()
    {
        //alert.SetActive(false);
    }

    void Update()
    {
        distance1 = Vector3.Distance(player.transform.position, thisroad1.transform.position);
        distance2 = Vector3.Distance(player.transform.position, thisroad2.transform.position);

        if (distance1 <= 3f)
        {
            Change1();
        }
        if (distance2 <= 20f)
        {
            Change2();
        }
    }

    public void Change1()
    {
        SceneManager.LoadScene("crosswalk");
    }

    private void Change2()
    {
        SceneManager.LoadScene("choiceroad");
    }

    IEnumerator delay1()
    {
        yield return _UIDelay1;

    }
}