using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;            //싱글톤. 사실 의미 없을 수도 있지만, 참조를 의도로 기능 활성화
    SpawnData spawnData;                            //적 스폰에 대한 데이터
    float waveTimer = 0, waveTime = 99f;            //기본적인 스폰 관련 타이머
    int waveIndex;
    bool isWaveTimerOn;                             //외부 객체에 의해 웨이브 타이머가 통제될 수 있음
    bool isSpawning;                                //몬스터 스폰 중에는 스폰 타이머가 돌아가지 않음

    private void Awake()
    {
        //싱글톤
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        WaveTimer();
    }

    void Init()
    {
        spawnData = this.gameObject.GetComponent<SpawnData>();
        SetWaveTimerOnState(false);
        SetIsSpawningState(false);
        waveIndex = 0;
    }

    void WaveDataLoad(SpawnData data)
    {
        //waveTime = 
    }

    void WaveTimer()
    {
        if (GetWaveTimerOnState() == true && GetIsSpawningState() == false)
        {
            waveTimer += Time.deltaTime;
        }
        if (waveTimer > waveTime)
        {
            SetTimer(0);
            WaveOn();
        }
    }

    

    void WaveOn()
    {
        StartCoroutine(SpawnCoroutine(spawnData));
    }

    IEnumerator SpawnCoroutine(SpawnData spawnInfo)
    {
        SetIsSpawningState(true);


        for (int i = 0; i < spawnData.spawnDataList.Count; i++)
        {

            yield return new WaitForSeconds(spawnInfo.spawnDataList[waveIndex].spawnDelay);

        }

        waveIndex++;
        SetIsSpawningState(false);
    }

    void SetTimer(float value)
    {
        waveTimer = value;
    }

    public void SetWaveTimerOnState(bool state)
    {
        isWaveTimerOn = state;
    }

    public bool GetWaveTimerOnState()
    {
        return isWaveTimerOn;
    }

    void SetIsSpawningState(bool state)
    {
        isSpawning = state;
    }

    bool GetIsSpawningState()
    {
        return isSpawning;
    }
}
