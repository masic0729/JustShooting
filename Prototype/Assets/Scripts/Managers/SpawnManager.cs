using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpawnData;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyList;
    public bool isEnemyA_Down;
    public static SpawnManager instance;            //싱글톤. 사실 의미 없을 수도 있지만, 참조를 의도로 기능 활성화\
    public Dictionary<string, GameObject> enemyName;
    SpawnData spawnData;                            //적 스폰에 대한 데이터
    float waveTimer = 0, waveTime = 99f;            //기본적인 스폰 관련 타이머
    int waveIndex;
    bool isWaveTimerOn;                             //외부 객체에 의해 웨이브 타이머가 통제될 수 있음
    bool isSpawning;                                //몬스터 스폰 중에는 스폰 타이머가 돌아가지 않음
    bool isBossSpawn;                               //보스 소환될 때 역시 타이머가 돌아가지 않음
    bool isWaveEnd;                                 //모든 웨이브가 끝나게 되면, 스탠스가 변경되어 게임이 끝남(승리)

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
        enemyName = new Dictionary<string, GameObject>();
        for(int i = 0; i <EnemyList.Length; i++)
        {
            enemyName[EnemyList[i].name] = EnemyList[i];
        }
        WaveDataLoad();

        SetWaveTimerOnState(true);
        SetIsSpawningState(false);
        waveIndex = 0;
        waveTime = spawnData.spawnDataList[0].waveDelay;
    }

    void WaveDataLoad()
    {
        waveTime = spawnData.spawnDataList[waveIndex].waveDelay;
    }

    void WaveTimer()
    {
        if (GetWaveTimerOnState() == true && GetIsSpawningState() == false)
        {
            waveTimer += Time.deltaTime;
        }
        if (waveTimer > waveTime && waveIndex != spawnData.spawnDataList.Count)
        {
            SetTimer(0);
            WaveOn();
        }
    }




    void WaveOn()
    {
        
        StartCoroutine(SpawnCoroutine(spawnData.spawnDataList[waveIndex]));
    }

    IEnumerator SpawnCoroutine(SpawnInfomation info)
    {
        SetIsSpawningState(true);

        if (info.enemyData.ToString() == EnemyData.Enemy_A.ToString())
        {
            int instanceRand = Random.Range(0, 2);
            isEnemyA_Down = instanceRand == 1 ? true : false;
        }

        for (int i = 0; i < info.spawnEnemyCount; i++)
        {
            Vector2 spawnPosition;
            if (info.isCustomPosition == false)
            {
                //이 부분은 원래 시스템 기획서에 있던 것
                float yInstance = -4.5f + (4.5f / info.spawnEnemyCount);
                spawnPosition = new Vector2(20f, yInstance + (9f * i / info.spawnEnemyCount));
            }
            else
            {
                float posX = info.ArrivePosition[i].x;
                //spawnPosition = new Vector2(info.spawnX_Value[i], info.spawnY_Value[i]);
                spawnPosition = new Vector2(20f + posX, info.ArrivePosition[i].y);
            }
            
            Enemy instanceEnemy = Instantiate(enemyName[info.enemyData.ToString()], spawnPosition, transform.rotation).GetComponent<Enemy>();
            if(info.isCustomPosition == true)
            {
                instanceEnemy.SetTargetPosition(info.ArrivePosition[i]);
            }

            if (info.spawnDelay != 0)
            {
                yield return new WaitForSeconds(info.spawnDelay);
            }

        }

        SetIsSpawningState(false);
        
        waveIndex++;
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
