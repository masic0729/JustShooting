using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Assets")]
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] AudioClip[] infClips;
    [SerializeField] AudioClip[] bgmClips;

    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> infDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();

    [Header("Audio Source Pooling")]
    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private int initialPoolSize = 10;
    private Queue<AudioSource> sfxPool = new Queue<AudioSource>();

    [Header("BGM")]
    [SerializeField] private AudioSource bgmSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionaries();
            InitializeSFXPool();
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        bgmSource = GetComponent<AudioSource>();
        PlayBGM("Title");
    }

    private void InitializeDictionaries()
    {
        for(int i = 0; i < sfxClips.Length; i++)
        {
            sfxDict[sfxClips[i].name] = sfxClips[i];
        }
        for (int i = 0; i < bgmClips.Length; i++)
        {
            bgmDict[bgmClips[i].name] = bgmClips[i];
        }
        for(int i = 0; i < infClips.Length; i++)
        {
            infDict[infClips[i].name] = infClips[i];
        }
    }

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
        StartCoroutine(ReturnToPoolAfterPlay(src));
    }

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
        StartCoroutine(ReturnToPoolAfterPlay(src));
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    private IEnumerator ReturnToPoolAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        source.Stop();
        source.clip = null;
        source.gameObject.SetActive(false);
        sfxPool.Enqueue(source);
    }



    /*public GameObject audioSourcePrefab;
    private Queue<AudioSource> pool = new Queue<AudioSource>();

    public AudioSource GetSource()
    {
        if (pool.Count > 0)
        {
            AudioSource src = pool.Dequeue();
            src.gameObject.SetActive(true);
            return src;
        }

        // 없으면 새로 생성
        GameObject instanceAudio = Instantiate(audioSourcePrefab, transform);
        return instanceAudio.GetComponent<AudioSource>();
    }

    public void ReturnToPool(AudioSource src)
    {
        src.Stop();
        src.clip = null;
        src.gameObject.SetActive(false);
        pool.Enqueue(src);
    }*/
}