using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerController : MonoBehaviour
{
    public GameObject hitBox;

    private void Awake()
    {
        this.RegisterListener(EventID.OffHitbox, (sender, param) =>
        {
            OffBox();
        });
    }
    public void OnBox()
    {
        hitBox.SetActive(true);
    }
    public void OffBox()
    {
        hitBox.SetActive(false);
    }
}
