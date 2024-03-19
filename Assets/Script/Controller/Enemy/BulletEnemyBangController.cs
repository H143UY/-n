using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyBangController : MonoBehaviour
{
    public GameObject HieuUngBang;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != this.gameObject.tag)
        {
            Destroy(this.gameObject);
            Instantiate(HieuUngBang,transform.position,transform.rotation);
        }
    }
}
