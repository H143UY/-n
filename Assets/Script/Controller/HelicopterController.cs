using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : ObjecController
{
    private Vector3 direc;
    void Start()
    {
        direc = new Vector3(0, 1, 0);
    }


    void Update()
    {
        Move(direc);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DiemA")
        {
            direc = new Vector3(0, -1, 0);
        }
        if (collision.gameObject.tag == "DiemB")
        {
            direc = new Vector3(0, 1, 0);
        }
    }
}
