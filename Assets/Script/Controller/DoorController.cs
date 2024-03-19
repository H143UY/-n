using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool CoChiaKhoa;
    public bool MoCua;
    public bool DongCua;
    private Vector3 Direc;
    private Rigidbody2D rb;
    public float TocDo;
    private float TimeCloseDoor;
    private Vector3 KhoangCach;
    private void Awake()
    {
        this.RegisterListener(EventID.ChiaKhoaMoCua, (sender, param) =>
        {
            CoChiaKhoa = true;
        });
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoCua = false;
        DongCua = false;
        CoChiaKhoa = false;
    }
    private void Update()
    {
        KhoangCach = this.gameObject.transform.position - MegamanController.Instance.transform.position;
        if (KhoangCach.x <= 3 && CoChiaKhoa)
        {
            MoCua = true;
            CoChiaKhoa = false;
        }
        if (MoCua == true)
        {
            OpenDoor();
        }
        CloseDoor();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "san")
        {
            Debug.Log("aa");
            DongCua = false;
            TimeCloseDoor = 0;
        }
        if (collision.gameObject.tag == "Diem 1")
        {
            Debug.Log("dong");
            DongCua = true;
            MoCua = false;
        }
    }
    private void CloseDoor()
    {
        if (DongCua == true)
        {
            TimeCloseDoor += Time.deltaTime;
            if (TimeCloseDoor >= 3)
            {
                this.gameObject.transform.position += new Vector3(0, -1, 0) * TocDo * 2 * Time.deltaTime;
            }
        }
    }
    private void OpenDoor()
    {
        this.gameObject.transform.position += new Vector3(0, 1, 0) * TocDo * Time.deltaTime;
    }
}
