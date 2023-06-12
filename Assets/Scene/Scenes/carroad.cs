using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class carroad : MonoBehaviour
{
    public GameObject subbox2;
    private WaitForSeconds _UIDelay1 = new WaitForSeconds(0.5f);

    // Start is called before the first frame update
    void Start()
    {
        subbox2.SetActive(false);
    }


    public void OnClickButton()
    {
        Debug.Log("Wrong choice!");
        StopAllCoroutines();
        StartCoroutine(delay2());
        StopAllCoroutines();
        Change();
    }

    public void Change()
    {
        SceneManager.LoadScene(0);
    }


    IEnumerator delay2()
    {
        subbox2.SetActive(true);
        yield return _UIDelay1;
        subbox2.SetActive(false);
    }

}
