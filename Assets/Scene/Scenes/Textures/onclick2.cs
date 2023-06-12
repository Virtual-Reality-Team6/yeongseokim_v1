using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onclick2 : MonoBehaviour
{
    public GameObject leftbutton;

    public GameObject one;

    public void changeMaterial()
    {
        if (one.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("try again");
        }
        
    }
}
