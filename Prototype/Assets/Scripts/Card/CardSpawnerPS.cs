using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIParticleSystem�� MaskableGraphic �迭�� �� �ʿ�

public class CardSpawnerPS : MonoBehaviour
{
    [SerializeField] ParticleSystem[] ps;

    Graphic[] uiGraphics;

    void Awake()
    {
        // ī�� �г� ���� UI �׷��� ���� Ȯ�� (UIParticleSystem ����)
        uiGraphics = GetComponentsInChildren<Graphic>(true);
    }

    void OnEnable()
    {
        // Ÿ�ӽ����� 0���� ī�� ���� ����
        Time.timeScale = 0f;

        for (int i = 0; i < ps.Length; i++)
        {
            if (!ps[i]) continue;

            // 1) ���� ����
            var main = ps[i].main;
            main.useUnscaledTime = true;
            main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

            // 2) ���� �ʱ�ȭ (Ǯ�� �ƴϾ ����)
            ps[i].Clear(true);
            ps[i].Simulate(0f, true, true, true);

            // 3) ���
            ps[i].Play(true);
        }

        //Debug.Log("ī�� ��ƼŬ ���� (unscaled)");
    }

    void Update()
    {
        // Ÿ�ӽ����� 0�� ���� �����Ӹ��� ���� �� ƽ�� �����ֱ�
        if (Time.timeScale == 0f)
        {
            float dt = Time.unscaledDeltaTime;
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i] && ps[i].isPlaying)
                    ps[i].Simulate(dt, true, false, false);
            }

            // UI ��ƼŬ�̸� ���ؽ� ������ ������ �ɾ� ���� ������Ʈ
            if (uiGraphics != null)
            {
                for (int i = 0; i < uiGraphics.Length; i++)
                    uiGraphics[i].SetVerticesDirty();
            }
        }
    }
}
