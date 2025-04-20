using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    public Material scrollMat;
    private Material instanceMaterial;
    private float time;

    bool isMoveBG = true;

    private void Start()
    {
        instanceMaterial = new Material(scrollMat);
        GetComponent<Renderer>().material = instanceMaterial;
    }

    void Update()
    {

        MoveBG();
    }

    void MoveBG()
    {
        if (isMoveBG == true)
        {
            time += Time.deltaTime;
            instanceMaterial.SetFloat("_ScrollTime", time);
        }
    }

    public void MoveBackgroundState(bool state)
    {
        isMoveBG = state;
    }
}
