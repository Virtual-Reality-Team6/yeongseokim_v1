using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossclear : MonoBehaviour
{
    public GameObject clearbutton;

    public void changeclear()
    {
        Debug.Log("clearbutton clicke");
        gameObject.SetActive(false);
    }
}
