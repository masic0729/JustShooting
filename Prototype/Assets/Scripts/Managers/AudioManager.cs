using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // 싱글톤 인스턴스

    [Header("Sound Assets")]
    [SerializeField] AudioClip[] sfxClips; // 일반 효과음
    [SerializeField] AudioClip[] infClips; // 인터페이스 효과음
    [SerializeField] AudioClip[] bgmClips; // 배경음

    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>(); // 효과음 딕셔너리
    private Dictionary<string, AudioClip> infDict = new Dictionary<string, AudioClip>(); // 인터페이스 효과음 딕셔너리
    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>(); // 배경음 딕셔너리

    [Header("Audio Source Pooling")]
    [SerializeField] private GameObject audioSourcePrefab; // 오디오 소스 프리팹
    [SerializeField] private int initialPoolSize = 10; // 초기 풀 크기
    private Queue<AudioSource> sfxPool = new Queue<AudioSource>(); // 오디오 소스 풀 큐

    [Header("BGM")]
    [SerializeField] private AudioSource bgmSource; // BGM 재생용 오디오 소스

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 싱글톤 할당
            DontDestroyOnLoad(gameObject); // 씬 전환 시 오브젝트 유지
            InitializeDictionaries(); // 오디오 클립 딕셔너리 초기화
            InitializeSFXPool(); // 오디오 소스 풀 초기화
        }
        else Destroy(gameObject); // 중복 방지
    }

    private void Start()
    {
        bgmSource = GetComponent<AudioSource>(); // 오디오 소스 가져오기
        PlayBGM("Title"); // 시작 시 타이틀 BGM 재생
    }

    // 배열 기반 클립들을 이름으로 딕셔너리에 저장
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

    // 오디오 소스를 미리 생성해서 풀에 저장
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

    // 풀에서 오디오 소스를 하나 꺼내옴. 없으면 새로 생성
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

    // 일반 효과음 재생
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
        StartCoroutine(ReturnToPoolAfterPlay(src)); // 재생 완료 후 풀에 반환
    }

    // 배경음 재생
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

    // 인터페이스 효과음 재생
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
        StartCoroutine(ReturnToPoolAfterPlay(src)); // 재생 후 반환
    }

    // BGM 정지
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // 재생이 끝난 오디오 소스를 풀에 반환하는 코루틴
    private IEnumerator ReturnToPoolAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        source.Stop();
        source.clip = null;
        source.gameObject.SetActive(false);
        sfxPool.Enqueue(source);
    }

    /* 아래는 과거 방식의 풀링 코드 백업 */
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
    }
    */
}
