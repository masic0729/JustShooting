using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpawnData;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyList;
    public static SpawnManager instance;

    private Dictionary<string, GameObject> enemyName;
    private SpawnData spawnData;

    [SerializeField]
    private float firstWaveTime = 1f;

    private float waveTimer = 0f;
    private float waveTime = 99f;

    private int waveGroupIndex = 0;
    private bool isWaveTimerOn = true;
    private bool isSpawning = false;
    private bool isBossSpawn = false;
    public bool isEnemyA_Down;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start() => Init();

    private void Update() => WaveTimer();

    void Init()
    {
        spawnData = GetComponent<SpawnData>();
        enemyName = new Dictionary<string, GameObject>();

        foreach (var enemy in EnemyList)
        {
            enemyName[enemy.name] = enemy;
        }

        waveGroupIndex = 0;
        waveTime = firstWaveTime;
    }

    void WaveDataLoad()
    {
        waveTime = spawnData.waveGroups[waveGroupIndex].nextWaveDelay;
    }

    void WaveTimer()
    {
        if (!isWaveTimerOn || isSpawning || isBossSpawn) return;

        waveTimer += Time.deltaTime;

        if (waveTimer > waveTime && waveGroupIndex < spawnData.waveGroups.Count)
        {
            SetTimer(0);
            WaveOn();
        }
    }

    void WaveOn()
    {
        StartCoroutine(SpawnCoroutineGroup(spawnData.waveGroups[waveGroupIndex]));
    }

    IEnumerator SpawnCoroutineGroup(WaveGroup group)
    {
        isSpawning = true;

        List<Coroutine> activeSpawns = new List<Coroutine>();

        foreach (var wave in group.wavesInGroup)
        {
            activeSpawns.Add(StartCoroutine(SpawnSingleWave(wave)));
        }

        foreach (var co in activeSpawns)
        {
            yield return co;
        }

        waveGroupIndex++;
        isSpawning = false;

        if (waveGroupIndex < spawnData.waveGroups.Count)
            WaveDataLoad();
    }

    IEnumerator SpawnSingleWave(SpawnInfomation info)
    {
        for (int i = 0; i < info.spawnEnemyCount; i++)
        {
            Vector2 spawnPosition;

            if (info.isCustomPosition)
            {
                float x = info.ArrivePosition[i].x;
                float y = info.isRandPositionY ? Random.Range(-4f, 4f) : info.ArrivePosition[i].y;
                spawnPosition = new Vector2(x + 20f, y);
            }
            else
            {
                spawnPosition = new Vector2(20f, Random.Range(-4f, 4f));
            }

            Enemy instanceEnemy = Instantiate(
                enemyName[info.enemyData.ToString()],
                spawnPosition,
                Quaternion.identity
            ).GetComponent<Enemy>();

            if (info.isCustomPosition && !info.isRandPositionY)
                instanceEnemy.SetTargetPosition(info.ArrivePosition[i]);

            if (info.spawnDelay > 0f)
                yield return new WaitForSeconds(info.spawnDelay);
        }
    }

    void SetTimer(float val) => waveTimer = val;

    public void SetIsBossSpawn(bool state)
    {
        isBossSpawn = state;
    }

    
}
