using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class choice : MonoBehaviour
{
    public GameObject Player;

    [Header("SubNotice")]
    public GameObject subbox1;
    public GameObject subbox2;
    //public Text subintext;
    //public Animator subani;

    public GameObject object1;
    public GameObject object2;

    private WaitForSeconds _UIDelay1 = new WaitForSeconds(0.5f);

    void Start()
    {
        subbox1.SetActive(false);
        subbox2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == object2)
                {
                    
                    subbox1.SetActive(false);
                    StopAllCoroutines();
                    StartCoroutine(delay2());
                    StopAllCoroutines();
                   
                    Debug.Log("Wrong choice!");
                    Change();
                }
                else if (hit.collider.gameObject == object1)
                {
                    
                    subbox2.SetActive(false);
                    StopAllCoroutines();
                    StartCoroutine(delay1());
                    StopAllCoroutines();
                    Debug.Log("right choice!");
                    Change();

                }
                

            }
        }

    }

    public void Change()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator delay1()
    {
        subbox1.SetActive(true);
        yield return _UIDelay1;
        subbox1.SetActive(false);
    }

    IEnumerator delay2()
    {
        subbox2.SetActive(true);
        yield return _UIDelay1;
        subbox2.SetActive(false);
    }
}