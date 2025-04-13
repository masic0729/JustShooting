using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    SpawnData spawnData;
    private Coroutine spawnCoroutine;
    private void Awake()
    {
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
        spawnData = this.gameObject.GetComponent<SpawnData>();
        spawnCoroutine = StartCoroutine(WaveCoroutine());
        Invoke("Test", 1.0f);
    }

    private void LateUpdate()
    {

    }

    void Test()
    {
        //StopCoroutine(spawnCoroutine);

    }

    void Test2()
    {
        //StartCoroutine(spawnCoroutine);
    }

    IEnumerator WaveCoroutine()
    {
        Repeat:
        Debug.Log("¾È³ç");

        /*for(int i = 0; i < spawnData.spawnDataList.Count; i++)
        {
            yield return new WaitForSeconds(0);

        }*/
        yield return new WaitForSeconds(0);

        goto Repeat;
    }
}
