using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetManaController : SetHpManaController
{
    public float MaxMana;
    public float CurrentMana;
    private void Awake()
    {
        this.RegisterListener(EventID.TruMana, (sender, param) =>
        {
            SubtractionMana(30); // ban tru mana
        });
        this.RegisterListener(EventID.HoiManaVaMau, (sender, param) =>
        {
            SetMana();
        });
        this.RegisterListener(EventID.Mana, (sender, param) =>
        {
            CurrentMana += 180;
        });
    }

    void Start()
    {
        CurrentMana = MaxMana;  
        SetMaxIndex(MaxMana);
    }
    private void Update()
    {
        if(CurrentMana <= 0)
        {
            CurrentMana = 0;
        }
    }

    public void SubtractionMana(float submana)
    {
        CurrentMana -= submana;
        SetIndex(CurrentMana);
    }
    public void SetMana()
    {
        CurrentMana += 5;
    }
}
