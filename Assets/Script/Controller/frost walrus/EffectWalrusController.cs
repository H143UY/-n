using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectWalrusController : MoveController
{
    public float TimeDestroy;

    void Start()
    {
    }
    private void Update()
    {
        Move(transform.up);
        TimeDestroy += Time.deltaTime;
        if(TimeDestroy >= 1)
        {
            Despan();
            TimeDestroy -= 0;
        }
    }

    private void Despan()
    {
        SmartPool.Instance.Despawn(this.gameObject);
    }

}
