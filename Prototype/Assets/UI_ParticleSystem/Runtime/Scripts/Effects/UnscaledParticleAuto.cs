using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions; // UIParticleSystem 네임스페이스

/// UIParticleSystem 공존 전용 드라이버
/// - Simulate() 재호출 절대 안 함(이중 진행 방지)
/// - timeScale=0에서 UI 리빌드 강제(Dirty + ForceUpdateCanvases)
/// - PlayOnAwake=false일 때 첫 프레임 Clear 충돌 회피(EndOfFrame 이후 시작)
/// - 소재/셰이더 누락 시 안전 보정(옵션)
[DisallowMultipleComponent]
public class UIParticlePauseDriver : MonoBehaviour
{
    [Header("수집/재생")]
    [SerializeField] bool autoCollect = true;          // 하위 UIParticleSystem 자동 수집
    [SerializeField] bool autoPlayOnEnable = true;     // 활성화 시 자동 재생
    [SerializeField] bool reinitOnEnable = true;       // 풀링 대비: 활성화 시 Stop+Clear

    [Header("정지 상태 UI 갱신")]
    [SerializeField] bool markUIDirtyOnPause = true;   // timeScale=0일 때 UI 더럽히기
    [SerializeField] bool forceCanvasFlush = true;     // 같은 프레임에 강제 플러시

    [Header("안전 보정(옵션)")]
    [SerializeField] bool fixPlayOnAwake = true;       // 내부 PS의 playOnAwake=true로 강제
    [SerializeField] bool ensureMaterial = true;       // 소재/셰이더 누락 시 기본 셰이더 지정

    [Header("수동 지정 시만 설정")]
    [SerializeField] List<UIParticleSystem> uiParticles = new List<UIParticleSystem>();

    Graphic[] cachedGraphics;
    bool startedOnce;

    void Awake()
    {
        if (autoCollect)
        {
            uiParticles.Clear();
            GetComponentsInChildren(true, uiParticles); // 비활성 포함
        }

        cachedGraphics = GetComponentsInChildren<Graphic>(true);

        // 안전 보정: 내부 PS 설정/머티리얼 점검
        foreach (var ups in uiParticles)
        {
            if (!ups) continue;

            var ps = ups.GetComponent<ParticleSystem>();
            if (ps)
            {
                var main = ps.main;

                // UIParticleSystem 첫 그리기에서 playOnAwake=false면 Clear를 쳐서 바로 꺼짐.
                if (fixPlayOnAwake && !main.playOnAwake)
                    main.playOnAwake = true;

                // offscreen에서도 진행되도록(시뮬은 UIParticleSystem이 한다)
                var cm = main.cullingMode;
                if (cm != ParticleSystemCullingMode.AlwaysSimulate)
                    main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

                // 기본 파라미터 안전값(Emission 0/수명 0 같은 실수 대비)
                var em = ps.emission;
                em.enabled = true;
                if (em.rateOverTime.constant <= 0f && em.burstCount == 0)
                    em.rateOverTime = 10f;
                if (main.startLifetime.constant <= 0f) main.startLifetime = 1f;
                if (main.maxParticles == 0) main.maxParticles = 1000;
                if (main.simulationSpeed == 0f) main.simulationSpeed = 1f;
                main.stopAction = ParticleSystemStopAction.None;
            }

            if (ensureMaterial)
            {
                // UIParticleSystem은 MaskableGraphic.material을 사용
                if (ups.material == null)
                {
                    // UIExtensions 셰이더 시도 → 실패 시 UI/Default
                    var sh = Shader.Find("UI Extensions/Particles/Additive");
                    if (!sh) sh = Shader.Find("UI/Default");
                    if (sh) ups.material = new Material(sh);
                }
            }
        }
    }

    void OnEnable()
    {
        if (reinitOnEnable)
        {
            foreach (var ups in uiParticles)
                ups?.StopParticleEmission(); // StopEmittingAndClear
        }

        if (autoPlayOnEnable)
            StartCoroutine(CoSafeStart());
    }

    IEnumerator CoSafeStart()
    {
        // UIParticleSystem이 첫 그리기에서 내부 초기화를 끝낼 때까지 대기
        yield return new WaitForEndOfFrame();
        if (!startedOnce) yield return null; // 한 프레임 더 여유

        foreach (var ups in uiParticles)
            ups?.StartParticleEmission();

        startedOnce = true;
    }

    void LateUpdate()
    {
        if (Time.timeScale != 0f) return;
        if (!markUIDirtyOnPause || cachedGraphics == null) return;

        // 시뮬은 UIParticleSystem이 언스케일드로 처리함 → 우리는 "보이는" 갱신만
        for (int i = 0; i < cachedGraphics.Length; i++)
        {
            var g = cachedGraphics[i];
            if (!g) continue;
            g.SetVerticesDirty();
            g.SetMaterialDirty();
        }
        if (forceCanvasFlush)
            Canvas.ForceUpdateCanvases();
    }

    // ===== 디버그/제어용 =====
    [ContextMenu("Play All")]
    public void PlayAll()
    {
        foreach (var ups in uiParticles) ups?.StartParticleEmission();
    }

    [ContextMenu("Pause All")]
    public void PauseAll()
    {
        foreach (var ups in uiParticles) ups?.PauseParticleEmission();
    }

    [ContextMenu("Stop All")]
    public void StopAll()
    {
        foreach (var ups in uiParticles) ups?.StopParticleEmission();
    }

    [ContextMenu("Emit 20 Now (Diag)")]
    public void EmitNow()
    {
        // Emission 무시 즉시 파티클 생성(이것도 안 보이면 레이어/캔버스 문제 확정)
        foreach (var ups in uiParticles)
        {
            var ps = ups ? ups.GetComponent<ParticleSystem>() : null;
            ps?.Emit(20);
        }
    }

    public void Register(UIParticleSystem ups)
    {
        if (ups && !uiParticles.Contains(ups))
            uiParticles.Add(ups);
    }
    public void Unregister(UIParticleSystem ups)
    {
        if (ups)
            uiParticles.Remove(ups);
    }
}
