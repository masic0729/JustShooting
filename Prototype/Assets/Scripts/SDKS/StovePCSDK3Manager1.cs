
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Stove.PCSDK.Base;
using static Stove.PCSDK.GameSupport;

public class STOVEPCSDK3Manager : MonoBehaviour
{
    public static STOVEPCSDK3Manager instance;

    [Header("UI")]
    public Text logText;

    [Header("STOVE Init")]
    [SerializeField] private string environment = "LIVE";
    [SerializeField] private string gameId = "GM-2783-6964EF6A_IND";
    [SerializeField] private string applicationKey = "7efc3ef1c4b4b420fdbfd4e0e466590673dbeedfbdc255467b12104debad1db1";

    [Header("Achievement Test")]
    //[SerializeField] private string testStatId = "TEST_0306";
    [SerializeField] private string loginFirst = "LOGIN_FIRST";
    [SerializeField] private string bossKill = "BOSSKILL";

    [SerializeField] private int bossSkillCount = 0;
    //[SerializeField] private int testStatValue = 1;

    [Header("Callback Loop")]
    [SerializeField] private float runCallbackInterval = 0.1f;

    private StovePCInitializeParam initParam;
    private OnModifyStatFinished onModifyStatFinished;
    private Coroutine runCallbackCoroutine;

    private bool hasTriggeredTestAchievement = false;
    private bool isBaseInitialized = false;
    private bool isGameSupportInitialized = false;

    bool isIAP_Inited = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SafeLog("Start 진입");

        if (logText == null)
        {
            Debug.LogError("[STOVE] logText가 연결되지 않았습니다.");
        }

        // 콜백 루프 시작
        StartRunCallbackLoop();

        initParam = new StovePCInitializeParam
        {
            environment = environment,
            gameId = gameId,
            applicationKey = applicationKey
        };

#if UNITY_EDITOR
        SafeLog("에디터 실행 감지 - STOVE SDK 정상 테스트는 빌드 후 런처 환경에서 확인");
        Debug.Log("[STOVE] Unity Editor에서는 정상 SDK 테스트가 어려울 수 있습니다.");
#endif

        SafeLog("준비중");

        Base_RestartAppIfNecessaryAsync(initParam, 30000, OnRestartAppIfNecessaryFinished);
        
    }

    private void StartRunCallbackLoop()
    {
        if (runCallbackCoroutine == null)
        {
            runCallbackCoroutine = StartCoroutine(RunCallbackCoroutine());
            Debug.Log("[STOVE] RunCallback 코루틴 시작");
        }
    }

    private IEnumerator RunCallbackCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(runCallbackInterval);

        while (true)
        {
            Base_RunCallback();
            yield return wait;
        }
    }

    private void OnRestartAppIfNecessaryFinished(CallbackResult callbackResult, bool restartAppIfNecessary)
    {
        Debug.Log($"[STOVE] RestartAppIfNecessary 결과: {callbackResult.result}, restart: {restartAppIfNecessary}");

        if (!callbackResult.result.IsSuccessful())
        {
            SafeLog($"런처 확인 실패: {callbackResult.result}");
            return;
        }

        if (restartAppIfNecessary)
        {
            SafeLog("스토브 런처로 실행해야 합니다.");
#if !UNITY_EDITOR
            Application.Quit();
#endif
            return;
        }

        SafeLog("런처 확인 성공, Base 초기화 시작");

        Base_Initialize(initParam, OnBaseInitializeFinished);
    }

    private void OnBaseInitializeFinished(CallbackResult callbackResult)
    {
        Debug.Log($"[STOVE] Base Initialize 결과: {callbackResult.result}");

        if (!callbackResult.result.IsSuccessful())
        {
            SafeLog($"Base SDK 초기화 실패: {callbackResult.result}");
            return;
        }

        isBaseInitialized = true;
        SafeLog("Base SDK 초기화 성공");

        InitializeGameSupportAndTriggerTest();

        //초기화 성공 이후 IAP 초기화 시작
        STOVEIAPManager.Instance.InitByBaseSDK();
    }

    private void InitializeGameSupportAndTriggerTest()
    {
        Result result = GameSupport_Initialize();
        Debug.Log($"[STOVE] GameSupport Initialize 결과: {result}");

        if (!result.IsSuccessful())
        {
            SafeLog($"GameSupport SDK 초기화 실패: {result}");
            return;
        }

        isGameSupportInitialized = true;
        SafeLog("GameSupport SDK 초기화 성공");


        onModifyStatFinished = OnModifyStatFinishedCallback;

        TriggerTestAchievementOnce();
    }

    public void TriggerTestAchievementOnce()
    {
        if (!isBaseInitialized)
        {
            SafeLog("Base SDK가 아직 초기화되지 않았습니다.");
            return;
        }

        if (!isGameSupportInitialized)
        {
            SafeLog("GameSupport SDK가 아직 초기화되지 않았습니다.");
            return;
        }

        if (hasTriggeredTestAchievement)
        {
            SafeLog("이미 테스트 업적을 발동했습니다.");
            return;
        }

        hasTriggeredTestAchievement = true;

        //GameSupport_ModifyStat(loginFitst, 1, onModifyStatFinished);
        SetPlayAction(loginFirst, 1, onModifyStatFinished);

        //SafeLog("테스트 업적용 Stat 갱신 요청 보냄");
    }

    /// <summary>
    /// 다양한 업적 관련 기능을 인게임 내 여러 경로를 통해 호출할 수 있도록 유도한다
    /// </summary>
    /// <param name="statID"></param>
    /// <param name="statValue"></param>
    /// <param name="method"></param>
    public void SetPlayAction(string statID, int statValue, OnModifyStatFinished method)
    {
        GameSupport_ModifyStat(statID, statValue, method);
    }

    private void OnModifyStatFinishedCallback(CallbackResult callbackResult, StovePCModifyStatValue stat)
    {
        Debug.Log($"[STOVE] ModifyStat 결과: {callbackResult.result}");

        if (callbackResult.result.IsSuccessful())
        {
            logText.text = "Stat 갱신 성공";
        }
        else
        {
            logText.text = ($"Stat 갱신 실패: {callbackResult.result}");
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("[STOVE] OnApplicationQuit 호출");

        if (runCallbackCoroutine != null)
        {
            StopCoroutine(runCallbackCoroutine);
            runCallbackCoroutine = null;
        }

        // 다른 SDK(IAP 등)를 쓴다면 그쪽 Uninitialize를 먼저 호출한 뒤 마지막에 Base_UnInitialize
        if (isGameSupportInitialized)
        {
            // GameSupport는 별도 Uninitialize가 없으면 생략
            isGameSupportInitialized = false;
        }

        if (isBaseInitialized)
        {
            Result result = Base_UnInitialize();
            Debug.Log($"[STOVE] Base_UnInitialize 결과: {result}");
            isBaseInitialized = false;
        }
    }

    private void SafeLog(string message)
    {
        Debug.Log("[STOVE] " + message);

        if (logText != null)
        {
            logText.text = message;
        }
    }

    public string GetBossKill() => bossKill;
    public void SetBossSkillCount(int value) => bossSkillCount = value;

    public int GetBossSkillCount() => bossSkillCount;

    public void SetIAP_Inited(bool state) => isIAP_Inited = state;
}