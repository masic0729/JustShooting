using UnityEngine;

public class PS_Unscaled_Proof : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;

    void Reset() { ps = GetComponent<ParticleSystem>(); }

    void Start()
    {
        if (!ps) ps = GetComponent<ParticleSystem>();

        // 핵심 세팅
        var main = ps.main;
        main.useUnscaledTime = true;
        main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

        // 보정: 정말 안 나오는 세팅들을 강제
        var emission = ps.emission; emission.enabled = true;
        if (emission.rateOverTime.constant <= 0f && ps.particleCount == 0)
            emission.rateOverTime = 20f;     // 발사량 보정
        if (main.startLifetime.constant <= 0f) main.startLifetime = 1f;
        if (main.maxParticles == 0) main.maxParticles = 1000;

        // 초기화 + 재생
        ps.Clear(true);
        ps.Simulate(0f, true, true, true);

        Time.timeScale = 0f;  // 정지
        ps.Play(true);        // 정지 중에도 재생됨
        Debug.Log("[Proof] Play sent at timeScale=0");
    }
}
