using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazeController : MonoBehaviour
{
    private float TimeToDestroy;
    public float TimeDestroy;
    void Start()
    {

    }
    void Update()
    {
        TimeDestroy -= Time.deltaTime;
        if (TimeDestroy <= 0)
        {
            this.PostEvent(EventID.LazeDestroy);
            SmartPool.Instance.Despawn(this.gameObject);
        }
    }
}
