using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions; // UIParticleSystem

/// <summary>
/// Ÿ�ӽ����� 0������ 'Ȯ���� ���̴�' ���� ������ + ��� ��Ÿ��.
/// - UIParticleSystem ����: Simulate() ��ȣ�� ����(�浹 ����)
/// - Emission/����/������ ���������� ���� (0�� �ܹ� ���� ����)
/// - UI�� EndOfFrame ���� �÷��̷� �ʱ�ȭ-���� Ÿ�̹� ȸ��
/// - ���� PS�� �ڵ� ó�� (�����ϵ�� ����)
/// </summary>
[DisallowMultipleComponent]
public class ParticleUnscaledSafeStarter : MonoBehaviour
{
    public enum EmitMode { RateOverTime, Bursts }

    [Header("Auto Collect")]
    [SerializeField] bool autoCollect = true;

    [Header("Preset (Ȯ���� ���̰�)")]
    [SerializeField] EmitMode emitMode = EmitMode.RateOverTime;
    [SerializeField] float rateOverTime = 25f;           // Rate ����� ��
    [SerializeField] short burstCount = 8;               // Burst ����� ��
    [SerializeField] float burstInterval = 0.05f;        // Burst �ݺ� ����
    [SerializeField] short burstCycles = 999;            // �ݺ� Ƚ��(��ǻ� ����)
    [SerializeField] float startLifetime = 0.8f;         // 0.5s �̻� ����
    [SerializeField] float startSpeed = 0f;              // ���簨������ 0~0.5
    [SerializeField] bool loop = true;
    [SerializeField] bool prewarm = true;

    [Header("Playback")]
    [SerializeField] bool reinitOnEnable = true;         // Ǯ�� ��� �ʱ�ȭ
    [SerializeField] bool autoPlayOnEnable = true;       // Ȱ��ȭ �� �ڵ� ���

    [Header("UI Refresh while Paused")]
    [SerializeField] bool markUIDirtyOnPause = true;
    [SerializeField] bool forceCanvasFlush = true;

    [Header("Safety")]
    [SerializeField] bool ensureMaterial = true;         // UIParticleSystem ��Ƽ���� ����
    [SerializeField] bool forceAlwaysSimulate = true;    // ī�޶�/������ũ������ ����

    [Header("Targets (���� ���� �ø�)")]
    [SerializeField] List<UIParticleSystem> uiParticles = new List<UIParticleSystem>();
    [SerializeField] List<ParticleSystem> worldParticles = new List<ParticleSystem>();

    Graphic[] cachedGraphics;

    void Awake()
    {
        if (autoCollect)
        {
            uiParticles.Clear();
            worldParticles.Clear();
            GetComponentsInChildren(true, uiParticles);      // ��Ȱ�� ����
            GetComponentsInChildren(true, worldParticles);   // ��Ȱ�� ����
        }

        cachedGraphics = GetComponentsInChildren<Graphic>(true);

        // ������ ����(���̴� �������� ����)
        foreach (var ups in uiParticles) ApplyPresetToUI(ups);
        foreach (var ps in worldParticles) if (ps && !HasUIParticle(ps)) ApplyPresetToWorld(ps);

        // UI ��Ƽ���� ����
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
                // VertexColor ���� 0�̸� �� ����: �⺻ ��� ���� ����
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
        // �ʱ�ȭ/ù �׸��� ����Ŭ�� �������� �� �� ���
        if (reinitOnEnable) ClearOnly();
        yield return new WaitForEndOfFrame();
        yield return null; // �� ������ �� ����

        // UI ���
        foreach (var ups in uiParticles)
            ups?.StartParticleEmission();

        // ���� ���
        foreach (var ps in worldParticles)
        {
            if (!ps || HasUIParticle(ps)) continue;
            ps.Clear(true);
            ps.Simulate(0f, true, true, true);
            // �����ϵ� ����(���� ���� �ɼ�)
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

        // UIParticleSystem�� ���ο��� unscaled�� �ù� �� �츮�� ������ ��
        for (int i = 0; i < cachedGraphics.Length; i++)
        {
            var g = cachedGraphics[i];
            if (!g) continue;
            g.SetVerticesDirty();
            g.SetMaterialDirty();
        }
        if (forceCanvasFlush) Canvas.ForceUpdateCanvases();
    }

    // ========= ������ ���� =========

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
        main.playOnAwake = true; // UIParticleSystem ù ������ Clear�� ������ �ʰ�
        if (forceAlwaysSimulate) main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

        var em = ps.emission;
        em.enabled = true;
        if (emitMode == EmitMode.RateOverTime)
        {
            em.rateOverTime = rateOverTime;
            em.SetBursts(null); // �ܹ� ���� ����
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
        main.useUnscaledTime = true; // ���� PS�� �����ϵ�� ���� ����
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
        // ���� ������Ʈ�� UIParticleSystem�� �پ� ������ UI ��η� ����
        return ps && ps.GetComponent<UIParticleSystem>() != null;
    }
}
