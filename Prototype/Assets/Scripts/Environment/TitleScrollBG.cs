using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScrollBG : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Material bgMaterial;
    private Vector2 offset;

    void Start()
    {
        // Renderer에서 머테리얼 가져오기 (공유 인스턴스 사용 방지 위해 new)
        bgMaterial = GetComponent<Image>().material;
        offset = bgMaterial.mainTextureOffset;
    }

    void Update()
    {
        offset.x -= scrollSpeed * Time.deltaTime;
        offset.y -= scrollSpeed * Time.deltaTime;
        if (offset.x <= -183)
        {
            offset.x = 0;
        }
        bgMaterial.mainTextureOffset = offset;
    }
}
