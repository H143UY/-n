using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startgamecontroller : MonoBehaviour
{
    public float TimeReturnCam;
    private bool vacham = false;
    private void Update()
    {
        if(vacham == true)
        {
            TimeReturnCam += Time.deltaTime;
            if(TimeReturnCam >=2)
            {
                this.PostEvent(EventID.SwichCamera1);
                this.PostEvent(EventID.Enddialogue);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.gameObject.tag == "Player")
        {
            if(vacham == false)
            {
                vacham = true;
                this.PostEvent(EventID.Isdialogue);
                this.PostEvent(EventID.SwichCamera2);
            }
        }
    }
}
