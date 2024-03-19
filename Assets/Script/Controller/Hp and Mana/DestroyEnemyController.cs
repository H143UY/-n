using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyController : MonoBehaviour
{
    public GameObject[] Item;
    public GameObject HieuUngDie;
    public float MaxHp;
    public float CurrentHp;
    public float GiapDich;
    public float HpToFillBar;
    void Start()
    {
        CurrentHp = MaxHp;
    }

    void Update()
    {
        HpToFillBar = CurrentHp / MaxHp;
        if(CurrentHp <= 0)
        {
            Die();
            CurrentHp = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.gameObject.tag == "bulletplayer")
        {
            TakeDamage(MegamanController.Instance.damage);
        }
    }
    public void Die()
    {
        SmartPool.Instance.Spawn(HieuUngDie,transform.position,transform.rotation);
        int x = Random.Range(0, Item.Length);
        SmartPool.Instance.Spawn(Item[x],transform.position,transform.rotation);
        Destroy(this.gameObject);
    }
    public void TakeDamage( float dame)
    {
        CurrentHp -= (dame- GiapDich); //tùy mỗi con thì sẽ có 1 lượng giáp khác nhau ,vì dame cố định nên sẽ tùy quái sẽ nhận ít dame hơn nhờ giáp
    }
}
