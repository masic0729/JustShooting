using UnityEngine;

public class Boss : Enemy
{
    protected override void Start()
    {
        base.Start();   
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        base.Init();
        SetIsBoss(true);
    }

    private void OnEnable()
    {
        //보스는 공통적으로 스폰되면 웨이브 자체가 멈추게 된다.
        StopWave();
    }

    protected void RestartWave()
    {
        Debug.Log("웨이브 재개");
        SpawnManager.instance.SetIsBossSpawn(false);
    }

    protected void StopWave()
    {
        Debug.Log("웨이브가 멈추는 기능");
        SpawnManager.instance.SetIsBossSpawn(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}