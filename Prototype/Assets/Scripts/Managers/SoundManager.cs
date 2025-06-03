using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤 인스턴스

    public float masterVolume = 1f;         // 전체 마스터 볼륨
    public float BGM_Volume = 1f;           // 배경음 볼륨
    public float Interface_Volume = 1f;     // UI 인터페이스 볼륨
    public float SFX_Volume = 1f;           // 효과음 볼륨

    public enum SoundType
    {
        BGM,        // 배경음
        Interface,  // UI 인터페이스 사운드
        SFX         // 일반 효과음
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(this.gameObject); // 중복 인스턴스 제거
        }
    }

    // 사운드 타입에 따른 최종 볼륨 값 계산
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
