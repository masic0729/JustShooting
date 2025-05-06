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
    EndBoss_A,
    EndBoss_Test
}
public class SpawnData : MonoBehaviour
{

    [System.Serializable]
    public class SpawnInfomation
    {
        public EnemyData enemyData;
        public int spawnEnemyCount;
        public float nextWaveDelay;
        public float spawnDelay;
        public bool isCustomPosition;
        public bool isRandPositionY;
        public float[] spawnX_Value;  
        public float[] spawnY_Value;  
        public Vector2[] ArrivePosition;
    }

    public List<SpawnInfomation> spawnDataList;
}