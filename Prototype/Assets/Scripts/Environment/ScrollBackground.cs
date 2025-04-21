using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    public Material[] scrollMat; //����� ���׸����� �迭ȭ�� ��
    private Material instanceMaterial; //�迭ȭ�� �����͸� ���� ��濡 �����Ϸ��� ������
    private float time; //���� �ð�

    bool isMoveBG = true; //��� ������ ����

    private void Start()
    {
        Init();
    }

    void Update()
    {

        MoveBG();
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    void Init()
    {
        instanceMaterial = new Material(scrollMat[0]);
        GetComponent<Renderer>().material = instanceMaterial;
    }

    /// <summary>
    /// ����� �����̴� ����, ���� �ð��� ������� �����δ�
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
    /// ����� ������ ���� ����
    /// </summary>
    /// <param name="state"></param>
    public void MoveBackgroundState(bool state)
    {
        isMoveBG = state;
    }

    /// <summary>
    /// �ӽÿ� ��� ���� �ڵ�
    /// </summary>
    /// <param name="index">���׸��� �������� �迭��</param>
    public void SetBackgroundData(int index)
    {
        instanceMaterial = scrollMat[index];
    }
}
