using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UIParticleUnscaledNudge : MonoBehaviour
{
    [SerializeField] ParticleSystem[] targets;         // 비우면 자동 수집
    [SerializeField] bool forceCanvasFlush = true;     // 정지 틱에 캔버스 강제 갱신

    Graphic[] uiGraphics;

    void Awake()
    {
        if (targets == null || targets.Length == 0)
            targets = GetComponentsInChildren<ParticleSystem>(true);

        // UI 파티클이 섞여 있을 수 있으니, 자식 Graphic 전부 캐싱 (비활성 포함)
        uiGraphics = GetComponentsInChildren<Graphic>(true);

        // A타입 핵심 세팅
        foreach (var p in targets)
        {
            if (!p) continue;
            var main = p.main;
            main.useUnscaledTime = true; // timeScale=0에서도 진행
            main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;
        }
    }

    void OnEnable()
    {
        foreach (var p in targets)
        {
            if (!p) continue;
            p.Clear(true);
            p.Simulate(0f, true, true, true);
            p.Play(true);
        }
    }

    void LateUpdate()
    {
        // 멈춤 구간에서 UI 강제 리빌드만 톡톡—시뮬레이트는 A타입에 맡김
        if (Time.timeScale != 0f || uiGraphics == null) return;

        for (int i = 0; i < uiGraphics.Length; i++)
        {
            // 메시 & 머티리얼 둘 다 더럽힘 → 다음 그리기 틱에 확실히 재빌드
            uiGraphics[i].SetVerticesDirty();
            uiGraphics[i].SetMaterialDirty();
        }

        if (forceCanvasFlush)
            Canvas.ForceUpdateCanvases(); // 이 프레임 안에 바로 반영

        GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, withChildren: true, restart: false, fixedTimeStep: false);

    }
}
