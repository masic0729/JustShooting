using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpawnData;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyList;
    public bool isEnemyA_Down;
    public static SpawnManager instance;                        //�̱���. ��� �ǹ� ���� ���� ������, ������ �ǵ��� ��� Ȱ��ȭ\
    public Dictionary<string, GameObject> enemyName;
    SpawnData spawnData;                                        //�� ������ ���� ������
    [SerializeField]
    float firstWaveTime;
    float waveTimer = 0, waveTime = 99f;                        //�⺻���� ���� ���� Ÿ�̸�
    int waveIndex;
    bool isWaveTimerOn;                                         //�ܺ� ��ü�� ���� ���̺� Ÿ�̸Ӱ� ������ �� ����
    bool isSpawning;                                            //���� ���� �߿��� ���� Ÿ�̸Ӱ� ���ư��� ����
    bool isBossSpawn;                                           //���� ��ȯ�� �� ���� Ÿ�̸Ӱ� ���ư��� ����
    bool isWaveEnd;                                             //��� ���̺갡 ������ �Ǹ�, ���Ľ��� ����Ǿ� ������ ����(�¸�)

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

        SetWaveTimerOnState(true);
        SetIsSpawningState(false);
        waveIndex = 0;
        waveTime = firstWaveTime;
    }

    void WaveDataLoad()
    {
        waveTime = spawnData.spawnDataList[waveIndex].nextWaveDelay;
    }

    void WaveTimer()
    {
        if (GetWaveTimerOnState() == true &&
            GetIsSpawningState() == false &&
            GetIsBossSpawn() == false)
        {
            waveTimer += Time.deltaTime;
        }
        if (waveTimer > waveTime && (waveIndex != spawnData.spawnDataList.Count))
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

        if (info.enemyData == EnemyData.Enemy_A)
        {
            int instanceRand = Random.Range(0, 2);
            isEnemyA_Down = instanceRand == 1;
        }

        for (int i = 0; i < info.spawnEnemyCount; i++)
        {
            Vector2 spawnPosition;
            float x;
            float y;
            x = info.ArrivePosition[i].x;
            y = info.ArrivePosition[i].y;

            if (info.isCustomPosition)
            {
                y = info.isRandPositionY ? Random.Range(-4f, 4f) : info.ArrivePosition[i].y;
                spawnPosition = new Vector2(x + 20f, y);
            }
            else
            {
                float yInstance = -4f + (4.5f / info.spawnEnemyCount);
                spawnPosition = new Vector2(20f, yInstance + (8f * i / info.spawnEnemyCount));
            }

            Enemy instanceEnemy = Instantiate(
                enemyName[info.enemyData.ToString()],
                spawnPosition,
                Quaternion.identity).GetComponent<Enemy>();

            // Target ��ġ�� ������ Ŀ������ ����
            if (info.isCustomPosition && !info.isRandPositionY)
            {
                instanceEnemy.SetTargetPosition(info.ArrivePosition[i]);
            }

            if (info.spawnDelay != 0)
                yield return new WaitForSeconds(info.spawnDelay);
        }

        waveIndex++;
        SetIsSpawningState(false);
        if(waveIndex < spawnData.spawnDataList.Count)
        {
            WaveDataLoad();
        }
    }

    public void SetIsBossSpawn(bool state)
    {
        isBossSpawn = state;
    }

    public bool GetIsBossSpawn()
    {
        return isBossSpawn;
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
