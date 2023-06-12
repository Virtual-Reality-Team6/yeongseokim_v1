using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearbutton : MonoBehaviour
{
    public GameObject subbox1;
    private WaitForSeconds _UIDelay1 = new WaitForSeconds(0.5f);

    // Start is called before the first frame update
    void Start()
    {
        subbox1.SetActive(false);
    }

    public void OnClickButton()
    {
        Debug.Log("right choice!");
        StopAllCoroutines();
        StartCoroutine(delay1());
        StopAllCoroutines();

        //Change();
    }


    IEnumerator delay1()
    {
        subbox1.SetActive(true);
        yield return _UIDelay1;
        subbox1.SetActive(false);
    }
}
