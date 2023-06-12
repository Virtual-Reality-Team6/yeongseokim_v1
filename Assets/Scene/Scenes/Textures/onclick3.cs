using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onclick3 : MonoBehaviour
{
    public GameObject downbutton;

    public GameObject one;
    public GameObject two;

    public void changeMaterial()
    {
        if (one.activeSelf == true || two.activeSelf == true)
        {
            Debug.Log("try again");
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
