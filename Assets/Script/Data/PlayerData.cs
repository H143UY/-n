using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float hp;
    
    public void SaveHp(float newhp)
    {
        if(newhp < hp)
        {
            hp = newhp;
            DataAccountPlayer.SaveDataPlayerData();
        }
    }
}
