using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.1f;
    private MeshRenderer bgMaterial;
    [SerializeField] Material[] backgroundParts;
    private Vector2 offset;

    void Start()
    {
        // Renderer���� ���׸��� �������� (���� �ν��Ͻ� ��� ���� ���� new)
        bgMaterial = GetComponent<MeshRenderer>();
        offset = bgMaterial.material.mainTextureOffset;
    }

    void Update()
    {
        offset.x += scrollSpeed * Time.deltaTime;
        if(offset.x >= 183)
        {
            offset.x = 0;
        }
        bgMaterial.material.mainTextureOffset = offset;

        /*if(Input.GetKeyDown(KeyCode.F8))
        {
            SetBackgroundPartByStage();
        }*/
    }

    
    /// <summary>
    /// �������� ��ȯ�� ���� ��� ��ȭ
    /// </summary>
    public void SetBackgroundPartByStage()
    {
        if (backgroundParts[1] != null)
        {
            bgMaterial.material = backgroundParts[1];
            offset = bgMaterial.material.mainTextureOffset;
        }
        else
        {
            bgMaterial.enabled = false;
        }
        
    }
}
