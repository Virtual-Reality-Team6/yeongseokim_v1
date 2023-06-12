using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onclick4 : MonoBehaviour
{
    public GameObject upbutton;

    public GameObject one;
    public GameObject two;
    public GameObject three;

    public void changeMaterial()
    {
        if (one.activeSelf == true || two.activeSelf == true || three.activeSelf==true)
        {
            Debug.Log("try again");
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
