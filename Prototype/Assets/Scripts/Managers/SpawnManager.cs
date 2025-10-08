using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpawnData;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyList; // 에디터에서 설정하는 적 프리팹 리스트
    public static SpawnManager instance;

    private Dictionary<string, GameObject> enemyName; // 이름 기준으로 적 프리팹 참조
    private SpawnData spawnData; // 스폰 데이터 참조

    [SerializeField]
    private float firstWaveTime = 1f; // 첫 웨이브 시작까지 시간

    private float waveTimer = 0f; // 현재 웨이브 타이머
    private float waveTime = 99f; // 다음 웨이브까지의 시간

    private int waveGroupIndex = 0; // 현재 웨이브 그룹 인덱스
    private bool isWaveTimerOn = true; // 타이머 동작 여부
    private bool isSpawning = false;   // 현재 스폰 중인지 여부
    private bool isBossSpawn = false;  // 보스 등장 여부
    public bool isEnemyA_Down;         // 특정 적 처치 여부 (외부 연동 예상)

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject); // 싱글톤 중복 방지
    }

    private void Start() => Init(); // 초기화 시작
    private void Update() => WaveTimer(); // 매 프레임마다 웨이브 타이머 체크

    void Init()
    {
        spawnData = GetComponent<SpawnData>(); // 스폰 데이터 연결
        enemyName = new Dictionary<string, GameObject>();

        foreach (var enemy in EnemyList)
        {
            enemyName[enemy.name] = enemy; // 이름으로 프리팹 등록
        }

        waveGroupIndex = 0;
        waveTime = firstWaveTime;
    }

    // 다음 웨이브 그룹의 딜레이를 세팅
    void WaveDataLoad()
    {
        waveTime = spawnData.waveGroups[waveGroupIndex].nextWaveDelay;
    }

    // 타이머가 조건을 만족하면 웨이브 실행
    void WaveTimer()
    {
        if (!isWaveTimerOn || isSpawning || isBossSpawn) return;

        waveTimer += Time.deltaTime;

        if (waveTimer > waveTime && waveGroupIndex < spawnData.waveGroups.Count)
        {
            SetTimer(0);
            WaveOn(); // 웨이브 실행
        }
    }

    // 웨이브 실행 코루틴 시작
    void WaveOn()
    {
        StartCoroutine(SpawnCoroutineGroup(spawnData.waveGroups[waveGroupIndex]));
    }

    // 한 웨이브 그룹의 모든 적들을 스폰 처리
    IEnumerator SpawnCoroutineGroup(WaveGroup group)
    {
        isSpawning = true;
        isEnemyA_Down = Random.Range(0, 2) == 1 ? false : true;
        List<Coroutine> activeSpawns = new List<Coroutine>();

        foreach (var wave in group.wavesInGroup)
        {
            activeSpawns.Add(StartCoroutine(SpawnSingleWave(wave))); // 동시에 여러 웨이브 스폰
        }

        foreach (var co in activeSpawns)
        {
            yield return co; // 개별 스폰 완료까지 대기
        }

        waveGroupIndex++;
        isSpawning = false;

        if (waveGroupIndex < spawnData.waveGroups.Count)
            WaveDataLoad(); // 다음 웨이브 준비
    }

    // 하나의 스폰 정보에 따른 적들을 개별 생성
    IEnumerator SpawnSingleWave(SpawnInfomation info)
    {
        for (int i = 0; i < info.spawnEnemyCount; i++)
        {
            Vector2 spawnPosition;

            if (info.isCustomPosition)
            {
                float x = info.ArrivePosition[i].x;
                float y = info.isRandPositionY ? Random.Range(-4f, 4f) : info.ArrivePosition[i].y;
                spawnPosition = new Vector2(x + 17f, y); // 오른쪽 화면 밖에서 생성
            }
            else
            {
                spawnPosition = new Vector2(17f, Random.Range(-4f, 4f)); // 무작위 Y 위치
            }

            // 적 프리팹 생성
            Enemy instanceEnemy = Instantiate(
                enemyName[info.enemyData.ToString()],
                spawnPosition,
                Quaternion.identity
            ).GetComponent<Enemy>();

            // 도착 위치 지정 (Y 고정일 경우)
            if (info.isCustomPosition && !info.isRandPositionY)
                instanceEnemy.SetTargetPosition(info.ArrivePosition[i]);

            // 적 간 딜레이
            if (info.spawnDelay > 0f)
                yield return new WaitForSeconds(info.spawnDelay);
        }
    }

    void SetTimer(float val) => waveTimer = val;

    public void SetIsBossSpawn(bool state)
    {
        isBossSpawn = state; // 외부에서 보스 스폰 상태 제어
    }
}
