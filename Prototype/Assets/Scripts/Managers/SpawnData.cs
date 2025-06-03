using System.Collections.Generic;
using UnityEngine;

// �� ������ ������ ������ (������ ����)
public enum EnemyData
{
    Enemy_A,
    Enemy_B,
    Enemy_C,
    Enemy_D,
    Enemy_Boss,
    MiddleBoss_Test,
    MiddleBoss_Test2,
    MiddleBoss_Test3,
    EndBoss_A,
    EndBoss_Test,
    EndBoss_Test2,
    EndBoss_Test3
}

// �� ���� ���� �����͸� ��� Ŭ����
public class SpawnData : MonoBehaviour
{
    // �ϳ��� �� ���� ���� ����
    [System.Serializable]
    public class SpawnInfomation
    {
        public EnemyData enemyData; // ������ �� ����
        public int spawnEnemyCount; // �� ����
        public float spawnDelay; // �� ���� �����Ǵ� ����
        public bool isCustomPosition; // ��ġ�� Ŀ���͸���¡���� ����
        public bool isRandPositionY; // Y ��ġ�� �������� ���� ����
        public float[] spawnX_Value; // Ŀ���� X ��ġ��
        public Vector2[] ArrivePosition; // ���� ��ġ (���Ͽ� ��)
    }

    // �ϳ��� ���̺� �׷� (�� ���� ���� �� ���� ����)
    [System.Serializable]
    public class WaveGroup
    {
        public float nextWaveDelay;  // ���� �׷���� ������
        public List<SpawnInfomation> wavesInGroup; // ���ÿ� ������ ���� ���� ����Ʈ
    }

    public List<WaveGroup> waveGroups; // ���� �� ���� ���� ����
}
