using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : MonoBehaviour
{
    private Animator animmator;
    public GameObject FireEffect;
    void Start()
    {
        animmator = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "san")
        {
            animmator.SetTrigger("idle");
        }
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(FireEffect,transform.position,transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
