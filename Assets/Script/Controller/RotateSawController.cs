using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class RotateSawController : ObjecController
{
    public float RotateSpeed;
    private Vector3 dir;
    void Start()
    {
        dir = new Vector3(-1, 0, 0);
    }
    void Update()
    {
        Move(dir);
        this.gameObject.transform.Rotate(0, 0, RotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DiemA")
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            dir = new Vector3(1, 0, 0);
        }
        if (collision.gameObject.tag == "DiemB")
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            dir = new Vector3(-1, 0, 0);
        }
    }

}
