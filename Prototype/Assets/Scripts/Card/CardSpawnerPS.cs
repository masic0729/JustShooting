using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIParticleSystem이 MaskableGraphic 계열일 때 필요

public class CardSpawnerPS : MonoBehaviour
{
    [SerializeField] ParticleSystem[] ps;

    Graphic[] uiGraphics;

    void Awake()
    {
        // 카드 패널 하위 UI 그래픽 전부 확보 (UIParticleSystem 포함)
        uiGraphics = GetComponentsInChildren<Graphic>(true);
    }

    void OnEnable()
    {
        // 타임스케일 0에서 카드 오픈 연출
        Time.timeScale = 0f;

        for (int i = 0; i < ps.Length; i++)
        {
            if (!ps[i]) continue;

            // 1) 메인 설정
            var main = ps[i].main;
            main.useUnscaledTime = true;
            main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

            // 2) 안전 초기화 (풀링 아니어도 권장)
            ps[i].Clear(true);
            ps[i].Simulate(0f, true, true, true);

            // 3) 재생
            ps[i].Play(true);
        }

        //Debug.Log("카드 파티클 시작 (unscaled)");
    }

    void Update()
    {
        // 타임스케일 0일 때는 프레임마다 직접 한 틱씩 돌려주기
        if (Time.timeScale == 0f)
        {
            float dt = Time.unscaledDeltaTime;
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i] && ps[i].isPlaying)
                    ps[i].Simulate(dt, true, false, false);
            }

            // UI 파티클이면 버텍스 갱신을 강제로 걸어 렌더 업데이트
            if (uiGraphics != null)
            {
                for (int i = 0; i < uiGraphics.Length; i++)
                    uiGraphics[i].SetVerticesDirty();
            }
        }
    }
}
