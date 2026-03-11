using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Stove.PCSDK.Base;
using static Stove.PCSDK.IAP;

public class STOVEIAPManager : MonoBehaviour
{
    public static STOVEIAPManager Instance;

    [Header("IAP")]
    [SerializeField] private string shopKey = "GM-_SH_LNKHCT";


    //STOVEPCPurchaseOperation, WebViewMode에 적절한 값을 할당
    private OnStartPurchaseFinished _onStartPurchaseFinished;

    [SerializeField] GameObject DonateButton;
    [SerializeField] GameObject DonateResultPanel;
    [SerializeField] Text ResultText;

    bool isAlreadyClickButton = false;                                                  //후원버튼을 눌러 창이 활성화하면 활성화한다


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    /// <summary>
    /// 결제 결과는 해당 함수로 할 수 있다
    /// </summary>
    public void DonateResult()
    {
        _onStartPurchaseFinished = (
        CallbackResult callbackResult,
        StovePCPurchaseResult purchase) =>
        {
            DonateResultPanel.SetActive(true);

            if (callbackResult.result.IsSuccessful())
            {
                // 상품 구매 성공 시 로직을 구현해 주세요.
                // 구매 성공 시 성공 창 노출
                ResultText.text = "후원 성공!!\n감사합니다!";
            }
            else
            {
                // 상품 구매 실패 시 로직을 구현해 주세요.
                // 구매 실패 시 실패 창 노출. 이때 오류 명 노출하면 좋을 듯
                ResultText.text = "후원 실패.\n문의 부탁드립니다.";
            }
        };
    }

    /// <summary>
    /// BaseSDK 관련 스크립트가 이 스크립트 초기화를 진행한다.
    /// 다시 말해  BaseSDK 초기화를 먼저 한 후에 진행해야 하므로, 이러하다
    /// </summary>
    public void IAP_Init()
    {
        // 4. IAP_Initialize 호출
        //    주의) IAP_Initialize 수행 전 Base_Initialize가 완료되어야 합니다.
        Result result = IAP_Initialize(shopKey);
        if (result.IsSuccessful())
        {
            // IAP SDK 초기화 성공 시 로직을 구현
            DonateButton.SetActive(true);
            //DonateResultInit();
        }
    }

    // 5. Button click이나 특정 이벤트 처리 함수 내에서 상품을 구매
    public void OnClickStartPurchase()
    {
        isAlreadyClickButton = true;

        // 6. 구현 의도에 맞게 상품 구매에 대한 적절한 operation을 설정
        var param = new StovePCPurchaseOption
        {
            operation = StovePCPurchaseOperation.WITH_WEBVIEW_AND_CONFIRM_RESULT,
            webviewMode = WebViewMode.EXTERNAL,
            webviewPosX = 0,                      // Webview의 x축 위치입니다. 기본값은 0입니다.
            webviewPosY = 0,                      // Webview의 y축 위치입니다. 기본값은 0입니다.
            webviewWidth = 800,                 // Webview 너비입니다. 기본값은 800입니다.
            webviewHeight = 600                 // Webview 너비입니다. 기본값은 800입니다.
        };

        // 7. 구매하고자 하는 상품에 대한 적절한 products (상품 목록), productsSize, purchaseOption 등을 설정하세요.
        StovePCOrderProduct[] productList = new StovePCOrderProduct[1];
        productList[0].productId = 11431;
        productList[0].salePrice = 1000;
        productList[0].quantity = 1;


        var startPurchaseParam = new StovePCStartPurchaseParam
        {
            products = productList,
            productsSize = (uint)productList.Length,
            option = param
        };

        // 8. IAP_StartPurchase 호출
        IAP_StartPurchase(startPurchaseParam, _onStartPurchaseFinished);
    }

    // 2. Button click이나 특정 이벤트 처리 함수 내에서 IAP_CloseAllPopups 진행합니다.
    public void OnClickIAPCloseAllPopups()
    {
        // 3. IAP_CloseAllPopups 호출
        IAP_CloseAllPopups();
        OnClickIAPUnInitialize();
    }

    // 2. Button click이나 특정 이벤트 처리 함수 내에서 IAP_UnInitialize 진행합니다.
    // 해당 함수는 OnClickIAPCloseAllPopups가 알아서 호출하니, 어떠한 곳에도 실행하지 말 것
    void OnClickIAPUnInitialize()
    {
        // 3. IAP_UnInitialize 호출
        //    주의) IAP_UnInitialize 수행 전 Base_UnInitialize가 완료되어야 합니다. 
        Result result = IAP_UnInitialize();
        if (result.IsSuccessful())
        {
            // IAPSDK 정리 API 호출 성공 시 로직을 구현해 주세요.
        }
        else
        {
            // IAPSDK 정리 API 호출 실패 시 로직을 구현해 주세요.
        }
    }

    void OnApplicationQuit()
    {
        OnClickIAPUnInitialize();
    }

    public void SetClickedButton(bool state) => isAlreadyClickButton = state;
}