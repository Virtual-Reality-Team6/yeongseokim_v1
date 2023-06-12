using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onclick6 : MonoBehaviour
{
    public GameObject leftbutton;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;


    public void changeMaterial()
    {
        if (one.activeSelf == true || two.activeSelf == true || three.activeSelf == true || four.activeSelf == true || five.activeSelf==true)
        {
            Debug.Log("try again");
        }
        else
        {

            gameObject.SetActive(false);
        }

    }
}
