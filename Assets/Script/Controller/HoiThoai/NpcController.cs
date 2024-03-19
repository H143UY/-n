using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public GameObject Key;
    public GameObject DiaChi;
    private bool Jump = false;
    private float TimeDestroy;
    private Rigidbody2D rigi;
    private Animator animator;
    private bool CreateKey;
    private void Awake()
    {
        this.RegisterListener(EventID.Enddialogue, (sender, param) =>
        {
            CreateKey = true;
        });
    }
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(Jump)
        {
            CreateKey = false;
            animator.Play("jump");
            rigi.AddForce(new Vector3(0, 5) * 10);
            TimeDestroy += Time.deltaTime;
            if(TimeDestroy >= 3)
            {
                Destroy(this.gameObject);
                Jump = false;
            }
        }
        if(CreateKey)
        {
            animator.Play("give the key");
        }
    }
   
    private void GiveKey()
    {
        SmartPool.Instance.Spawn(Key, DiaChi.transform.position, DiaChi.transform.rotation);
    }
    private void nhay()
    {
        Jump = true;
    }
}
