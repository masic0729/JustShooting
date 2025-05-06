using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public float masterVolume = 1f;
    public float BGM_Volume = 1f;
    public float Interface_Volume = 1f;
    public float SFX_Volume = 1f;

    

    public enum SoundType
    {
        BGM,
        Interface,
        SFX
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float GetVolume(SoundType type)
    {
        switch (type)
        {
            case SoundType.BGM: return BGM_Volume * masterVolume;
            case SoundType.SFX: return SFX_Volume * masterVolume;
            case SoundType.Interface: return Interface_Volume * masterVolume;
        }
        Debug.Log("예외 발생");
        return 0f;
    }
}
