using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioClip[] BGM_Array;
    [SerializeField] AudioClip[] interactionArray;
    [SerializeField] AudioClip[] resultArray;

    [SerializeField] AudioSource audioBGM;
    [SerializeField] AudioSource audioInteraction;
    [SerializeField] AudioSource audioResult;

    public enum EnumBGM
    {
        BGM_Title,
        BGM_Stage1,
        BGM_Stage2,
        BGM_Stage3,
        BGM_Stage4
    }

    public enum EnumInteraction
    {
        InteractionMouseClick,
        InteractionPlugConnect,
        InteractionPlugRotate
    }

    public enum EnumResult
    {
        StageClear,
        StageFailure
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        InitAudioSources();
        PlayBGM(EnumBGM.BGM_Stage1);
    }


    void InitAudioSources()
    {
        audioBGM.loop = true;
    }

    public void PlayInteraction(EnumInteraction interaction)
    {

        audioInteraction.clip = interactionArray[(int)interaction];

        audioInteraction.Play();

    }

    public void PlayResult(EnumResult result)
    {
        audioResult.clip = resultArray[(int)result];

        audioResult.Play();

    }

    public void PlayBGM(EnumBGM bgm)
    {
        audioBGM.clip = BGM_Array[(int)bgm];
        audioBGM.Play();
    }
    public void PauseBGM()
    {
        audioBGM.Pause();
    }

    public void ContinueBGM()
    {
        audioBGM.UnPause();
    }

}
