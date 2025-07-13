using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    public Material[] scrollMat; // 배경의 메테리얼을 배열화한 것 (다양한 배경 전환용으로 사용 가능)
    private Material instanceMaterial; // 배열화된 데이터를 실제 배경에 적용하려는 변수
    private float time; // 실행 시간 누적용 (스크롤 제어)

    bool isMoveBG = true; // 배경 움직임 권한 (일시정지 처리 등에서 사용)

    private void Start()
    {
        Init();
    }

    void Update()
    {
        MoveBG(); // 매 프레임 배경 이동 시도
    }

    /// <summary>
    /// 초기화
    /// </summary>
    void Init()
    {
        instanceMaterial = new Material(scrollMat[0]); // 배열 중 첫 배경으로 시작
        GetComponent<Renderer>().material = instanceMaterial; // 실제 오브젝트에 메테리얼 적용
    }

    /// <summary>
    /// 배경을 움직이는 원리, 실행 시간을 기반으로 움직인다
    /// </summary>
    void MoveBG()
    {
        if (isMoveBG == true)
        {
            time += Time.deltaTime;
            instanceMaterial.SetFloat("_ScrollTime", time); // 쉐이더에서 '_ScrollTime'을 기준으로 텍스처 UV 스크롤
        }
    }

    /// <summary>
    /// 배경의 움직임 권한 관리
    /// </summary>
    /// <param cardName="state">true: 움직임 활성화, false: 멈춤</param>
    public void MoveBackgroundState(bool state)
    {
        isMoveBG = state;
    }

    /// <summary>
    /// 임시용 배경 변경 코드
    /// </summary>
    /// <param cardName="index">메테리얼 데이터의 배열값</param>
    public void SetBackgroundData(int index)
    {
        instanceMaterial = scrollMat[index]; // 메테리얼 참조만 바뀌며, 실제 적용은 Init와 다르게 Renderer 갱신이 없음
        //실제로 렌더링에 적용되지 않으므로, 아래 한 줄 필요할 수 있음
        // GetComponent<Renderer>().material = instanceMaterial;
    }
}
