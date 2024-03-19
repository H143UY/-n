using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Model.Core.Sound;
using Controller.LoadData;

public class LoadResourceController : SingletonMono<LoadResourceController>
{
    private Dictionary<string, Object> _resourceCache = new Dictionary<string, Object>();

    #region LoadMethod

    private T Load<T>(string path, string fileName) where T : Object
    {
        var fullPath = Path.Combine(path, fileName);
        if (_resourceCache.ContainsKey(fullPath) is false)
        {
            _resourceCache.Add(fullPath, TryToLoad<T>(path, fileName));
        }

        return _resourceCache[fullPath] as T;
    }

    private static T TryToLoad<T>(string path, string fileName) where T : Object
    {
        var fullPath = Path.Combine(path, fileName);
        var result = Resources.Load<T>(fullPath);
        return result;
    }

    #endregion

    #region Public Load Method

  

    public AudioClip LoadAudioClip(SoundType soundType)
    {
        return Load<AudioClip>(ResourcesFolderPath.SoundFolder, soundType.ToString());
    }


    #endregion

    #region LoadDataAsset

    public SoundCollection LoadDataSoundsGun()
    {
        var path = string.Format(ResourcesFolderPath.DataFolder, ResourcesFolderPath.DataSounds);
        return Load<SoundCollection>(path, "SoundsData");
    }


    #endregion
}
