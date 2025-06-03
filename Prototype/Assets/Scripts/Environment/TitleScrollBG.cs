using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScrollBG : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // 배경이 움직이는 속도 조절 변수
    private Material bgMaterial; // 배경에 적용된 머테리얼을 저장하는 변수
    private Vector2 offset; // 텍스처 오프셋을 저장해 스크롤 효과를 주기 위한 변수

    void Start()
    {
        // Renderer에서 머테리얼 가져오기 (공유 인스턴스 사용 방지 위해 new)
        bgMaterial = GetComponent<Image>().material; // UI 이미지에서 머테리얼 참조
        offset = bgMaterial.mainTextureOffset; // 현재 머테리얼의 텍스처 오프셋 값을 저장
    }

    void Update()
    {
        // 텍스처 오프셋의 x, y 값을 일정 속도로 감소시켜 좌하단 방향으로 스크롤
        offset.x -= scrollSpeed * Time.deltaTime;
        offset.y -= scrollSpeed * Time.deltaTime;

        // 일정 거리 이상 이동하면 다시 처음 위치로 리셋하여 무한 반복 효과
        if (offset.x <= -183)
        {
            offset.x = 0;
        }

        // 변경된 오프셋 값을 머테리얼에 반영하여 스크롤 효과 적용
        bgMaterial.mainTextureOffset = offset;
    }
}
