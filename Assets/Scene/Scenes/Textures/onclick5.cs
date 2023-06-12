using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onclick5 : MonoBehaviour
{
    public GameObject downbutton;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;


    public void changeMaterial()
    {
        if (one.activeSelf == true || two.activeSelf == true || three.activeSelf == true || four.activeSelf == true)
        {
            Debug.Log("try again");
        }
        else{
            gameObject.SetActive(false);
        }
    }

}
