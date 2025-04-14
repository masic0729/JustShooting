using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;            //�̱���. ��� �ǹ� ���� ���� ������, ������ �ǵ��� ��� Ȱ��ȭ
    SpawnData spawnData;                            //�� ������ ���� ������
    float waveTimer = 0, waveTime = 99f;            //�⺻���� ���� ���� Ÿ�̸�
    int waveIndex;
    bool isWaveTimerOn;                             //�ܺ� ��ü�� ���� ���̺� Ÿ�̸Ӱ� ������ �� ����
    bool isSpawning;                                //���� ���� �߿��� ���� Ÿ�̸Ӱ� ���ư��� ����

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
