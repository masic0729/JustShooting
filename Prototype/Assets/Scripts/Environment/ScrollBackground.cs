using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    public Material[] scrollMat; //배경의 메테리얼을 배열화한 것
    private Material instanceMaterial; //배열화된 데이터를 실제 배경에 적용하려는 데이터
    private float time; //실행 시간

    bool isMoveBG = true; //배경 움직임 권한

    private void Start()
    {
        Init();
    }

    void Update()
    {

        MoveBG();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    void Init()
    {
        instanceMaterial = new Material(scrollMat[0]);
        GetComponent<Renderer>().material = instanceMaterial;
    }

    /// <summary>
    /// 배경을 움직이는 원리, 실행 시간을 기반으로 움직인다
    /// </summary>
    void MoveBG()
    {
        if (isMoveBG == true)
        {
            time += Time.deltaTime;
            instanceMaterial.SetFloat("_ScrollTime", time);
        }
    }

    /// <summary>
    /// 배경의 움직임 권한 관리
    /// </summary>
    /// <param name="state"></param>
    public void MoveBackgroundState(bool state)
    {
        isMoveBG = state;
    }

    /// <summary>
    /// 임시용 배경 변경 코드
    /// </summary>
    /// <param name="index">메테리얼 데이터의 배열값</param>
    public void SetBackgroundData(int index)
    {
        instanceMaterial = scrollMat[index];
    }
}
