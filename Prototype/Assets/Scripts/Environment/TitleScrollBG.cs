using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScrollBG : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // ����� �����̴� �ӵ� ���� ����
    private Material bgMaterial; // ��濡 ����� ���׸����� �����ϴ� ����
    private Vector2 offset; // �ؽ�ó �������� ������ ��ũ�� ȿ���� �ֱ� ���� ����

    void Start()
    {
        // Renderer���� ���׸��� �������� (���� �ν��Ͻ� ��� ���� ���� new)
        bgMaterial = GetComponent<Image>().material; // UI �̹������� ���׸��� ����
        offset = bgMaterial.mainTextureOffset; // ���� ���׸����� �ؽ�ó ������ ���� ����
    }

    void Update()
    {
        // �ؽ�ó �������� x, y ���� ���� �ӵ��� ���ҽ��� ���ϴ� �������� ��ũ��
        offset.x -= scrollSpeed * Time.deltaTime;
        offset.y -= scrollSpeed * Time.deltaTime;

        // ���� �Ÿ� �̻� �̵��ϸ� �ٽ� ó�� ��ġ�� �����Ͽ� ���� �ݺ� ȿ��
        if (offset.x <= -183)
        {
            offset.x = 0;
        }

        // ����� ������ ���� ���׸��� �ݿ��Ͽ� ��ũ�� ȿ�� ����
        bgMaterial.mainTextureOffset = offset;
    }
}
