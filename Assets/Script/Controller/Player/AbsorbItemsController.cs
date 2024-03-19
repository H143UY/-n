using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbItemsController : MonoBehaviour
{
    [Header("item")]
    //set
    public float UpSpeed;
    public float UpForce;
    private float TimeSpeedCoolDown;
    private bool AnItemSpeed;
    private void Start()
    {
        AnItemSpeed = false;
    }

    private void Update()
    {
        if (AnItemSpeed)
        {
            TangToc();
        }
    }
    private void TangToc()
    {      
        TimeSpeedCoolDown += Time.deltaTime;
        if (TimeSpeedCoolDown >= 10)
        {
            MegamanController.Instance.speed -= UpSpeed;
            MegamanController.Instance.JumpForce -= UpForce;
            TimeSpeedCoolDown = 0;
            AnItemSpeed = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //item
        if (collision.gameObject.tag == "Item heart")
        {
            this.PostEvent(EventID.Heart);
        }
        if (collision.gameObject.tag == "mana")
        {
            this.PostEvent(EventID.Mana);
        }
        if (collision.gameObject.tag == "key")
        {
            this.PostEvent(EventID.Key);
        }
        if (collision.gameObject.tag == "key take door")
        {
            this.PostEvent(EventID.ChiaKhoaMoCua);
        }
        if (collision.gameObject.tag == "speed")
        {
            MegamanController.Instance.speed += UpSpeed;
            MegamanController.Instance.JumpForce += UpForce;
            AnItemSpeed = true;
        }
        if (collision.gameObject.tag == "bua thor")
        {
            MegamanController.Instance.damage += 5;
        }
        if (collision.gameObject.tag == "bullet bang")
        {

        }
    }
}
