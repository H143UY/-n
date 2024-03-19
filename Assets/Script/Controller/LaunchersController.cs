using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchersController : MonoBehaviour
{
    private bool IsJump;
    public Animator animator;
    public float JumForce;
    void Start()
    {
        IsJump = false;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetBool("jump", IsJump);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            IsJump = true;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumForce , ForceMode2D.Impulse);
        }
    }
    public void Jump()
    {
        IsJump =false;
    }
}
