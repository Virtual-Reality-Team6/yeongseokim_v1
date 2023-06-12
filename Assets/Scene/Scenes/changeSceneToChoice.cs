using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeSceneToChoice : MonoBehaviour
{
    public GameObject player;
    public GameObject thisroad;
    //public GameObject alert;
    private float distance;

    private WaitForSeconds _UIDelay1 = new WaitForSeconds(2.0f);

    void Start()
    {
        //alert.SetActive(false);
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, thisroad.transform.position);

        if (distance <= 20f)
        {
            //alert.SetActive(true);
            //StartCoroutine(delay1());
            //StopAllCoroutines();
            //alert.SetActive(false);
            //StartCoroutine(delay1());
            //StopAllCoroutines();
            Change();
        }
    }

    private void Change()
    {
        SceneManager.LoadScene("choice");
    }

    IEnumerator delay1()
    {
        yield return _UIDelay1;

    }
}


