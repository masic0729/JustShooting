using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions; // UIParticleSystem

/// <summary>
/// 타임스케일 0에서도 '확실히 보이는' 안전 프리셋 + 재생 스타터.
/// - UIParticleSystem 공존: Simulate() 재호출 없음(충돌 방지)
/// - Emission/수명/루프를 안전값으로 보정 (0초 단발 증발 방지)
/// - UI는 EndOfFrame 이후 플레이로 초기화-삭제 타이밍 회피
/// - 월드 PS도 자동 처리 (언스케일드로 진행)
/// </summary>
[DisallowMultipleComponent]
public class ParticleUnscaledSafeStarter : MonoBehaviour
{
    public enum EmitMode { RateOverTime, Bursts }

    [Header("Auto Collect")]
    [SerializeField] bool autoCollect = true;

    [Header("Preset (확실히 보이게)")]
    [SerializeField] EmitMode emitMode = EmitMode.RateOverTime;
    [SerializeField] float rateOverTime = 25f;           // Rate 모드일 때
    [SerializeField] short burstCount = 8;               // Burst 모드일 때
    [SerializeField] float burstInterval = 0.05f;        // Burst 반복 간격
    [SerializeField] short burstCycles = 999;            // 반복 횟수(사실상 루프)
    [SerializeField] float startLifetime = 0.8f;         // 0.5s 이상 권장
    [SerializeField] float startSpeed = 0f;              // 존재감용으로 0~0.5
    [SerializeField] bool loop = true;
    [SerializeField] bool prewarm = true;

    [Header("Playback")]
    [SerializeField] bool reinitOnEnable = true;         // 풀링 대비 초기화
    [SerializeField] bool autoPlayOnEnable = true;       // 활성화 시 자동 재생

    [Header("UI Refresh while Paused")]
    [SerializeField] bool markUIDirtyOnPause = true;
    [SerializeField] bool forceCanvasFlush = true;

    [Header("Safety")]
    [SerializeField] bool ensureMaterial = true;         // UIParticleSystem 머티리얼 보정
    [SerializeField] bool forceAlwaysSimulate = true;    // 카메라/오프스크린에도 진행

    [Header("Targets (수동 지정 시만)")]
    [SerializeField] List<UIParticleSystem> uiParticles = new List<UIParticleSystem>();
    [SerializeField] List<ParticleSystem> worldParticles = new List<ParticleSystem>();

    Graphic[] cachedGraphics;

    void Awake()
    {
        if (autoCollect)
        {
            uiParticles.Clear();
            worldParticles.Clear();
            GetComponentsInChildren(true, uiParticles);      // 비활성 포함
            GetComponentsInChildren(true, worldParticles);   // 비활성 포함
        }

        cachedGraphics = GetComponentsInChildren<Graphic>(true);

        // 프리셋 적용(보이는 세팅으로 강제)
        foreach (var ups in uiParticles) ApplyPresetToUI(ups);
        foreach (var ps in worldParticles) if (ps && !HasUIParticle(ps)) ApplyPresetToWorld(ps);

        // UI 머티리얼 보정
        if (ensureMaterial)
        {
            foreach (var ups in uiParticles)
            {
                if (!ups) continue;
                if (!ups.material)
                {
                    var sh = Shader.Find("UI Extensions/Particles/Additive");
                    if (!sh) sh = Shader.Find("UI/Default");
                    if (sh) ups.material = new Material(sh);
                }
                // VertexColor 알파 0이면 안 보임: 기본 흰색 유지 권장
            }
        }
    }

    void OnEnable()
    {
        if (autoPlayOnEnable)
            StartCoroutine(CoSafePlay());
        else if (reinitOnEnable)
            ClearOnly();
    }

    IEnumerator CoSafePlay()
    {
        // 초기화/첫 그리기 사이클을 지나가게 한 뒤 재생
        if (reinitOnEnable) ClearOnly();
        yield return new WaitForEndOfFrame();
        yield return null; // 한 프레임 더 여유

        // UI 경로
        foreach (var ups in uiParticles)
            ups?.StartParticleEmission();

        // 월드 경로
        foreach (var ps in worldParticles)
        {
            if (!ps || HasUIParticle(ps)) continue;
            ps.Clear(true);
            ps.Simulate(0f, true, true, true);
            // 언스케일드 진행(엔진 제공 옵션)
#if UNITY_2019_3_OR_NEWER
            var main = ps.main;
            main.useUnscaledTime = true;
#endif
            ps.Play(true);
        }
    }

    void ClearOnly()
    {
        foreach (var ups in uiParticles) ups?.StopParticleEmission();
        foreach (var ps in worldParticles)
        {
            if (!ps || HasUIParticle(ps)) continue;
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    void LateUpdate()
    {
        if (Time.timeScale != 0f) return;
        if (!markUIDirtyOnPause || cachedGraphics == null) return;

        // UIParticleSystem은 내부에서 unscaled로 시뮬 → 우리는 렌더만 톡
        for (int i = 0; i < cachedGraphics.Length; i++)
        {
            var g = cachedGraphics[i];
            if (!g) continue;
            g.SetVerticesDirty();
            g.SetMaterialDirty();
        }
        if (forceCanvasFlush) Canvas.ForceUpdateCanvases();
    }

    // ========= 프리셋 적용 =========

    void ApplyPresetToUI(UIParticleSystem ups)
    {
        if (!ups) return;
        var ps = ups.GetComponent<ParticleSystem>();
        if (!ps) return;

        var main = ps.main;
        main.loop = loop;
        main.prewarm = prewarm;
        main.startLifetime = startLifetime;
        main.startSpeed = startSpeed;
        main.playOnAwake = true; // UIParticleSystem 첫 프레임 Clear에 씹히지 않게
        if (forceAlwaysSimulate) main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

        var em = ps.emission;
        em.enabled = true;
        if (emitMode == EmitMode.RateOverTime)
        {
            em.rateOverTime = rateOverTime;
            em.SetBursts(null); // 단발 세팅 삭제
        }
        else
        {
#if UNITY_2019_3_OR_NEWER
            var b = new ParticleSystem.Burst(0.01f, burstCount, burstCount, burstCycles, burstInterval);
#else
            var b = new ParticleSystem.Burst(0.01f, burstCount);
#endif
            em.rateOverTime = 0f;
            em.SetBursts(new ParticleSystem.Burst[] { b });
        }
    }

    void ApplyPresetToWorld(ParticleSystem ps)
    {
        var main = ps.main;
        main.loop = loop;
        main.prewarm = prewarm;
        main.startLifetime = startLifetime;
        main.startSpeed = startSpeed;
#if UNITY_2019_3_OR_NEWER
        main.useUnscaledTime = true; // 월드 PS는 언스케일드로 직접 진행
#endif
        if (forceAlwaysSimulate) main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

        var em = ps.emission;
        em.enabled = true;
        if (emitMode == EmitMode.RateOverTime)
        {
            em.rateOverTime = rateOverTime;
            em.SetBursts(null);
        }
        else
        {
#if UNITY_2019_3_OR_NEWER
            var b = new ParticleSystem.Burst(0.01f, burstCount, burstCount, burstCycles, burstInterval);
#else
            var b = new ParticleSystem.Burst(0.01f, burstCount);
#endif
            em.rateOverTime = 0f;
            em.SetBursts(new ParticleSystem.Burst[] { b });
        }
    }

    bool HasUIParticle(ParticleSystem ps)
    {
        // 같은 오브젝트에 UIParticleSystem가 붙어 있으면 UI 경로로 본다
        return ps && ps.GetComponent<UIParticleSystem>() != null;
    }
}
