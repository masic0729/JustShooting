using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData : MonoBehaviour
{
    //public GameObject[] spawnEnemy;

    [System.Serializable]
    public class SpawnInfomation
    {
        public enum EnemyData
        {
            Enemy_A,
            Enemy_B,
            Enemy_C,
            Enemy_D,
            Enemy_Boss
        }

        public EnemyData enemyData;
        public int spawnEnemyCount;

        public float waveDelay;
        public float spawnDelay;
        public bool isCustomPosition;
        public float[] spawnX_Value;
        public float[] spawnY_Value;
    }

    public List<SpawnInfomation> spawnDataList;
}