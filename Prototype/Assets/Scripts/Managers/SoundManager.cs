using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // �̱��� �ν��Ͻ�

    public float masterVolume = 1f;         // ��ü ������ ����
    public float BGM_Volume = 1f;           // ����� ����
    public float Interface_Volume = 1f;     // UI �������̽� ����
    public float SFX_Volume = 1f;           // ȿ���� ����

    public enum SoundType
    {
        BGM,        // �����
        Interface,  // UI �������̽� ����
        SFX         // �Ϲ� ȿ����
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // ���� �ٲ� ����
        }
        else
        {
            Destroy(this.gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    // ���� Ÿ�Կ� ���� ���� ���� �� ���
    public float GetVolume(SoundType type)
    {
        switch (type)
        {
            case SoundType.BGM: return BGM_Volume * masterVolume;
            case SoundType.SFX: return SFX_Volume * masterVolume;
            case SoundType.Interface: return Interface_Volume * masterVolume;
        }

        Debug.Log("���� �߻�");
        return 0f;
    }
}
