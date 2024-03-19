using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletIceController : MonoBehaviour
{
    public Animator anim;
    public bool vacham = false;
    void Start()
    {
        anim =  GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(vacham)
        {
            anim.SetTrigger("vacham");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "san")
        {
            vacham = true;
        }
    }
}
