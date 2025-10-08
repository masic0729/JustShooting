using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpawnData;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyList; // �����Ϳ��� �����ϴ� �� ������ ����Ʈ
    public static SpawnManager instance;

    private Dictionary<string, GameObject> enemyName; // �̸� �������� �� ������ ����
    private SpawnData spawnData; // ���� ������ ����

    [SerializeField]
    private float firstWaveTime = 1f; // ù ���̺� ���۱��� �ð�

    private float waveTimer = 0f; // ���� ���̺� Ÿ�̸�
    private float waveTime = 99f; // ���� ���̺������ �ð�

    private int waveGroupIndex = 0; // ���� ���̺� �׷� �ε���
    private bool isWaveTimerOn = true; // Ÿ�̸� ���� ����
    private bool isSpawning = false;   // ���� ���� ������ ����
    private bool isBossSpawn = false;  // ���� ���� ����
    public bool isEnemyA_Down;         // Ư�� �� óġ ���� (�ܺ� ���� ����)

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject); // �̱��� �ߺ� ����
    }

    private void Start() => Init(); // �ʱ�ȭ ����
    private void Update() => WaveTimer(); // �� �����Ӹ��� ���̺� Ÿ�̸� üũ

    void Init()
    {
        spawnData = GetComponent<SpawnData>(); // ���� ������ ����
        enemyName = new Dictionary<string, GameObject>();

        foreach (var enemy in EnemyList)
        {
            enemyName[enemy.name] = enemy; // �̸����� ������ ���
        }

        waveGroupIndex = 0;
        waveTime = firstWaveTime;
    }

    // ���� ���̺� �׷��� �����̸� ����
    void WaveDataLoad()
    {
        waveTime = spawnData.waveGroups[waveGroupIndex].nextWaveDelay;
    }

    // Ÿ�̸Ӱ� ������ �����ϸ� ���̺� ����
    void WaveTimer()
    {
        if (!isWaveTimerOn || isSpawning || isBossSpawn) return;

        waveTimer += Time.deltaTime;

        if (waveTimer > waveTime && waveGroupIndex < spawnData.waveGroups.Count)
        {
            SetTimer(0);
            WaveOn(); // ���̺� ����
        }
    }

    // ���̺� ���� �ڷ�ƾ ����
    void WaveOn()
    {
        StartCoroutine(SpawnCoroutineGroup(spawnData.waveGroups[waveGroupIndex]));
    }

    // �� ���̺� �׷��� ��� ������ ���� ó��
    IEnumerator SpawnCoroutineGroup(WaveGroup group)
    {
        isSpawning = true;
        isEnemyA_Down = Random.Range(0, 2) == 1 ? false : true;
        List<Coroutine> activeSpawns = new List<Coroutine>();

        foreach (var wave in group.wavesInGroup)
        {
            activeSpawns.Add(StartCoroutine(SpawnSingleWave(wave))); // ���ÿ� ���� ���̺� ����
        }

        foreach (var co in activeSpawns)
        {
            yield return co; // ���� ���� �Ϸ���� ���
        }

        waveGroupIndex++;
        isSpawning = false;

        if (waveGroupIndex < spawnData.waveGroups.Count)
            WaveDataLoad(); // ���� ���̺� �غ�
    }

    // �ϳ��� ���� ������ ���� ������ ���� ����
    IEnumerator SpawnSingleWave(SpawnInfomation info)
    {
        for (int i = 0; i < info.spawnEnemyCount; i++)
        {
            Vector2 spawnPosition;

            if (info.isCustomPosition)
            {
                float x = info.ArrivePosition[i].x;
                float y = info.isRandPositionY ? Random.Range(-4f, 4f) : info.ArrivePosition[i].y;
                spawnPosition = new Vector2(x + 17f, y); // ������ ȭ�� �ۿ��� ����
            }
            else
            {
                spawnPosition = new Vector2(17f, Random.Range(-4f, 4f)); // ������ Y ��ġ
            }

            // �� ������ ����
            Enemy instanceEnemy = Instantiate(
                enemyName[info.enemyData.ToString()],
                spawnPosition,
                Quaternion.identity
            ).GetComponent<Enemy>();

            // ���� ��ġ ���� (Y ������ ���)
            if (info.isCustomPosition && !info.isRandPositionY)
                instanceEnemy.SetTargetPosition(info.ArrivePosition[i]);

            // �� �� ������
            if (info.spawnDelay > 0f)
                yield return new WaitForSeconds(info.spawnDelay);
        }
    }

    void SetTimer(float val) => waveTimer = val;

    public void SetIsBossSpawn(bool state)
    {
        isBossSpawn = state; // �ܺο��� ���� ���� ���� ����
    }
}
