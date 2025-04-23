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
        //������ ���������� �����Ǹ� ���̺� ��ü�� ���߰� �ȴ�.
        StopWave();
    }

    protected void RestartWave()
    {
        Debug.Log("���̺� �簳");
        SpawnManager.instance.SetIsBossSpawn(false);
    }

    protected void StopWave()
    {
        Debug.Log("���̺갡 ���ߴ� ���");
        SpawnManager.instance.SetIsBossSpawn(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}