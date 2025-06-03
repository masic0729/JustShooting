using System.Collections.Generic;
using UnityEngine;

// 적 종류를 정의한 열거형 (스폰에 사용됨)
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

// 적 스폰 관련 데이터를 담는 클래스
public class SpawnData : MonoBehaviour
{
    // 하나의 적 스폰 정보 단위
    [System.Serializable]
    public class SpawnInfomation
    {
        public EnemyData enemyData; // 스폰할 적 종류
        public int spawnEnemyCount; // 적 개수
        public float spawnDelay; // 이 적이 스폰되는 간격
        public bool isCustomPosition; // 위치를 커스터마이징할지 여부
        public bool isRandPositionY; // Y 위치를 랜덤으로 줄지 여부
        public float[] spawnX_Value; // 커스텀 X 위치들
        public Vector2[] ArrivePosition; // 도착 위치 (패턴용 등)
    }

    // 하나의 웨이브 그룹 (한 번에 여러 적 스폰 가능)
    [System.Serializable]
    public class WaveGroup
    {
        public float nextWaveDelay;  // 다음 그룹까지 딜레이
        public List<SpawnInfomation> wavesInGroup; // 동시에 나오는 스폰 정보 리스트
    }

    public List<WaveGroup> waveGroups; // 여러 개 동시 실행 가능
}
