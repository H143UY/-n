using System.Collections.Generic;
using UnityEngine;
using SoundResources;
using Model.Core.Sound;

[CreateAssetMenu(menuName = "Data/sounds", fileName = "SoundsData")]
[SerializeField]

public class SoundCollection : ScriptableObject
{
    public List<SoundData> listSounds = new List<SoundData>();

    public SoundType GetSoundGun(int idGun,int countTap)
    {
        var sound = listSounds[idGun].sounds[countTap];
        return sound;
    }

    public int listSoundinGun(int idGun)
    {
        var num = listSounds[idGun].sounds.Count;
        return num;
    }
}
