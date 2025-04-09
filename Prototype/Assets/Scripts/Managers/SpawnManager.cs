using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Vector2 SpawnPos;
    public GameObject[] Enemies;

    public enum SpawnType
    {
        Type_A = 0,
        Type_B,
        Type_C,
        Type_D = 3
    }
    public SpawnType spawnType;

    private void Start()
    {
        SpawnPos = new Vector2(11, 0);
        StartCoroutine(Wave01(Enemies[0], SpawnPos, 10, 0.5f));
    }

    public IEnumerator Wave01(GameObject enemy, Vector2 spawnPos, int spawnCount, float spawnDelay)
    {
        for(int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(spawnDelay);

            Instantiate(enemy, spawnPos, transform.rotation);
        }
    }
}
