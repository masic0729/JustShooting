using System.Collections.Generic;
using UnityEngine;

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

public class SpawnData : MonoBehaviour
{
    [System.Serializable]
    public class SpawnInfomation
    {
        public EnemyData enemyData;
        public int spawnEnemyCount;
        public float spawnDelay;
        public bool isCustomPosition;
        public bool isRandPositionY;
        public float[] spawnX_Value;
        public Vector2[] ArrivePosition;
    }

    [System.Serializable]
    public class WaveGroup
    {
        public float nextWaveDelay;  // 다음 그룹까지 딜레이
        public List<SpawnInfomation> wavesInGroup;
    }

    public List<WaveGroup> waveGroups; // 여러 개 동시 실행 가능
}
