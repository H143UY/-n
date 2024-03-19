using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class BeeController : ObjecController
{
    private bool IsAttack;
    private Vector3 dir;
    private bool FindPlayer;
    private float dist;
    private Animator anim;
    public Transform PosAttack;
    public float distance = 0.85f;
    public LayerMask LayerEnem;
    private bool ishit;
    public float timer;
    public GameObject[] StartPos;
    private float TimeToRandom;
    private int x;
    private float directionFlip;
    void Start()
    {
        x = Random.Range(0, StartPos.Length);
        anim = GetComponent<Animator>();
        IsAttack = false;
        FindPlayer = false;
    }
    void Update()
    {
        RandomDir();
        Move(dir);
        if (FindPlayer && ishit == false)
        {
            dir = (MegamanController.Instance.transform.position - this.gameObject.transform.position) + new Vector3(2, 4, 1);
        }
        else if (ishit == true)
        {
            dir = new Vector3(0, 0, 0);
        }
        else if (FindPlayer == false)
        {
            dir = StartPos[x].transform.position - this.gameObject.transform.position;
        }
        CheckPlayer();
        CheckAttack();
        anim.SetBool("attack", IsAttack);
        if (ishit == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ishit = false;
            }
        }
        Flip();
    }
    private void CheckPlayer()
    {
        dist = Vector3.Distance(MegamanController.Instance.transform.position, this.gameObject.transform.position);
        if (dist < 13)
        {
            FindPlayer = true;
        }
        else
        {
            FindPlayer = false;
        }
    }
    private void Flip()
    {
        directionFlip = MegamanController.Instance.transform.position.x - this.gameObject.transform.position.x;
        if (directionFlip < 0)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void CheckAttack()
    {
        IsAttack = Physics2D.OverlapCircle(PosAttack.position, distance, LayerEnem);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(PosAttack.position, distance);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bulletplayer")
        {
            ishit = true;
        }
    }
    private void RandomDir()
    {
        TimeToRandom += Time.deltaTime;
        if(TimeToRandom >= 4)
        {
            x = Random.Range(0, StartPos.Length);
            TimeToRandom = 0;
        }
    }
}
