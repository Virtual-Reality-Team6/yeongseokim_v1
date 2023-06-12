using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;

    Vector3 moveVec;

    Animator anim;

    private Vector3 targetPosition;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        //targetPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        //transform.position += moveVec * speed * Time.deltaTime;
        if(one.activeSelf == false || two.activeSelf == false || three.activeSelf == false || four.activeSelf == false || five.activeSelf == false || six.activeSelf == false)
        {
            anim.SetBool("Run", moveVec == Vector3.zero);
        }
        
        //anim.SetBool("Walk", wDown);

        transform.LookAt(transform.position+moveVec);

    }
    


}
