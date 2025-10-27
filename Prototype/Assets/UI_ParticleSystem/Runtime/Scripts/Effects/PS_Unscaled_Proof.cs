using UnityEngine;

public class PS_Unscaled_Proof : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;

    void Reset() { ps = GetComponent<ParticleSystem>(); }

    void Start()
    {
        if (!ps) ps = GetComponent<ParticleSystem>();

        // �ٽ� ����
        var main = ps.main;
        main.useUnscaledTime = true;
        main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

        // ����: ���� �� ������ ���õ��� ����
        var emission = ps.emission; emission.enabled = true;
        if (emission.rateOverTime.constant <= 0f && ps.particleCount == 0)
            emission.rateOverTime = 20f;     // �߻緮 ����
        if (main.startLifetime.constant <= 0f) main.startLifetime = 1f;
        if (main.maxParticles == 0) main.maxParticles = 1000;

        // �ʱ�ȭ + ���
        ps.Clear(true);
        ps.Simulate(0f, true, true, true);

        Time.timeScale = 0f;  // ����
        ps.Play(true);        // ���� �߿��� �����
        Debug.Log("[Proof] Play sent at timeScale=0");
    }
}
