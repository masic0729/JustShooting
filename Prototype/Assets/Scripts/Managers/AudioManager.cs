using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // �̱��� �ν��Ͻ�

    [Header("Sound Assets")]
    [SerializeField] AudioClip[] sfxClips; // �Ϲ� ȿ����
    [SerializeField] AudioClip[] infClips; // �������̽� ȿ����
    [SerializeField] AudioClip[] bgmClips; // �����

    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>(); // ȿ���� ��ųʸ�
    private Dictionary<string, AudioClip> infDict = new Dictionary<string, AudioClip>(); // �������̽� ȿ���� ��ųʸ�
    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>(); // ����� ��ųʸ�

    [Header("Audio Source Pooling")]
    [SerializeField] private GameObject audioSourcePrefab; // ����� �ҽ� ������
    [SerializeField] private int initialPoolSize = 10; // �ʱ� Ǯ ũ��
    private Queue<AudioSource> sfxPool = new Queue<AudioSource>(); // ����� �ҽ� Ǯ ť

    [Header("BGM")]
    [SerializeField] private AudioSource bgmSource; // BGM ����� ����� �ҽ�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // �̱��� �Ҵ�
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ������Ʈ ����
            InitializeDictionaries(); // ����� Ŭ�� ��ųʸ� �ʱ�ȭ
            InitializeSFXPool(); // ����� �ҽ� Ǯ �ʱ�ȭ
        }
        else Destroy(gameObject); // �ߺ� ����
    }

    private void Start()
    {
        bgmSource = GetComponent<AudioSource>(); // ����� �ҽ� ��������
        PlayBGM("Title"); // ���� �� Ÿ��Ʋ BGM ���
    }

    // �迭 ��� Ŭ������ �̸����� ��ųʸ��� ����
    private void InitializeDictionaries()
    {
        for (int i = 0; i < sfxClips.Length; i++)
        {
            sfxDict[sfxClips[i].name] = sfxClips[i];
        }
        for (int i = 0; i < bgmClips.Length; i++)
        {
            bgmDict[bgmClips[i].name] = bgmClips[i];
        }
        for (int i = 0; i < infClips.Length; i++)
        {
            infDict[infClips[i].name] = infClips[i];
        }
    }

    // ����� �ҽ��� �̸� �����ؼ� Ǯ�� ����
    private void InitializeSFXPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject instanceAudio = Instantiate(audioSourcePrefab, transform);
            AudioSource source = instanceAudio.GetComponent<AudioSource>();
            instanceAudio.SetActive(false);
            sfxPool.Enqueue(source);
        }
    }

    // Ǯ���� ����� �ҽ��� �ϳ� ������. ������ ���� ����
    private AudioSource GetSFXSource()
    {
        if (sfxPool.Count > 0)
        {
            AudioSource src = sfxPool.Dequeue();
            src.gameObject.SetActive(true);
            return src;
        }
        else
        {
            GameObject instance = Instantiate(audioSourcePrefab, transform);
            return instance.GetComponent<AudioSource>();
        }
    }

    // �Ϲ� ȿ���� ���
    public void PlaySFX(string name)
    {
        if (!sfxDict.TryGetValue(name, out AudioClip clip))
        {
            Debug.LogWarning($"SFX '{name}' not found.");
            return;
        }

        AudioSource src = GetSFXSource();
        src.volume = SoundManager.instance.SFX_Volume * SoundManager.instance.masterVolume;
        src.clip = clip;
        src.Play();
        StartCoroutine(ReturnToPoolAfterPlay(src)); // ��� �Ϸ� �� Ǯ�� ��ȯ
    }

    // ����� ���
    public void PlayBGM(string name, bool loop = true)
    {
        if (!bgmDict.TryGetValue(name, out AudioClip clip))
        {
            Debug.LogWarning($"BGM '{name}' not found.");
            return;
        }
        bgmSource.volume = SoundManager.instance.BGM_Volume * SoundManager.instance.masterVolume;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // �������̽� ȿ���� ���
    public void PlayINF(string name)
    {
        if (!infDict.TryGetValue(name, out AudioClip clip))
        {
            Debug.LogWarning($"INF '{name}' not found.");
            return;
        }

        AudioSource src = GetSFXSource();
        src.volume = SoundManager.instance.Interface_Volume * SoundManager.instance.masterVolume;

        src.clip = clip;
        src.Play();
        StartCoroutine(ReturnToPoolAfterPlay(src)); // ��� �� ��ȯ
    }

    // BGM ����
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // ����� ���� ����� �ҽ��� Ǯ�� ��ȯ�ϴ� �ڷ�ƾ
    private IEnumerator ReturnToPoolAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        source.Stop();
        source.clip = null;
        source.gameObject.SetActive(false);
        sfxPool.Enqueue(source);
    }

    /* �Ʒ��� ���� ����� Ǯ�� �ڵ� ��� */
    /*
    public GameObject audioSourcePrefab;
    private Queue<AudioSource> pool = new Queue<AudioSource>();

    public AudioSource GetSource()
    {
        if (pool.Count > 0)
        {
            AudioSource src = pool.Dequeue();
            src.gameObject.SetActive(true);
            return src;
        }

        // ������ ���� ����
        GameObject instanceAudio = Instantiate(audioSourcePrefab, transform);
        return instanceAudio.GetComponent<AudioSource>();
    }

    public void ReturnToPool(AudioSource src)
    {
        src.Stop();
        src.clip = null;
        src.gameObject.SetActive(false);
        pool.Enqueue(src);
    }
    */
}
