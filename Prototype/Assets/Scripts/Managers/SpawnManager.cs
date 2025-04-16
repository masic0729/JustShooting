using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpawnData;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyList;
    public bool isEnemyA_Down;
    public static SpawnManager instance;            //�̱���. ��� �ǹ� ���� ���� ������, ������ �ǵ��� ��� Ȱ��ȭ\
    public Dictionary<string, GameObject> enemyName;
    SpawnData spawnData;                            //�� ������ ���� ������
    float waveTimer = 0, waveTime = 99f;            //�⺻���� ���� ���� Ÿ�̸�
    int waveIndex;
    bool isWaveTimerOn;                             //�ܺ� ��ü�� ���� ���̺� Ÿ�̸Ӱ� ������ �� ����
    bool isSpawning;                                //���� ���� �߿��� ���� Ÿ�̸Ӱ� ���ư��� ����
    bool isBossSpawn;                               //���� ��ȯ�� �� ���� Ÿ�̸Ӱ� ���ư��� ����
    bool isWaveEnd;                                 //��� ���̺갡 ������ �Ǹ�, ���Ľ��� ����Ǿ� ������ ����(�¸�)

    private void Awake()
    {
        //�̱���
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
                //�� �κ��� ���� �ý��� ��ȹ���� �ִ� ��
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
