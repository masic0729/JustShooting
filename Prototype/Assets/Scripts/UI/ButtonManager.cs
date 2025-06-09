using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// UI 버튼과 관련된 기능을 처리하는 클래스
public class ButtonManager : MonoBehaviour
{
    private GameObject ClickedButton; // 사용자가 클릭한 버튼을 저장하는 변수

    public float scaleMultiplier = 1.1f; // 마우스 오버 시 버튼 크기 확대 비율
    private Vector3 buttonDefaultScale = new Vector3(1, 1, 1); // 버튼 원래 크기

    // 씬 전환 처리
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // 씬 로드
        Time.timeScale = 1.0f; // 시간 다시 흐르게 설정
    }

    // UI 비활성화 (예: 메뉴 닫기)
    public void HideUI(GameObject target)
    {
        target.SetActive(false); // 오브젝트 비활성화
        Time.timeScale = 1.0f; // 시간 다시 흐르게 설정
    }

    // UI 활성화 (예: 메뉴 열기)
    public void ShowUI(GameObject target)
    {
        target.SetActive(true); // 오브젝트 활성화
        Time.timeScale = 0.0f; // 시간 정지 (일시정지 효과)
    }

    // 버튼 클릭 시 효과음 출력
    public void ButtonSoundOutPut(string soundName)
    {
        if (soundName == "")
        {
            soundName = "ClickSample"; // 기본 클릭 사운드
        }
        AudioManager.Instance.PlayINF(soundName); // 효과음 재생
    }

    // 배경음 변경
    public void ChangeBGM(string soundName)
    {
        AudioManager.Instance.PlayBGM(soundName); // 새로운 배경음 재생
    }

    // 버튼 텍스트를 다른 텍스트로 복사
    public void TransText(GameObject target)
    {
        // 클릭한 버튼의 텍스트를 대상 텍스트에 복사
        target.GetComponent<TextMeshProUGUI>().text = ClickedButton.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    // 클릭된 버튼 정보 저장
    public void ClickButtonData(GameObject button)
    {
        ClickedButton = button;
    }

    // 마우스가 버튼 위에 올라갔을 때 크기 키우는 효과 시작
    public void OnButtonEnter(GameObject button)
    {
        StopAllCoroutines(); // 이전 애니메이션 중지
        StartCoroutine(ScaleTo(button)); // 크기 확대 코루틴 시작
    }

    // 마우스가 버튼에서 벗어났을 때 원래 크기로 복귀
    public void OnButtonExit(GameObject button)
    {
        StopAllCoroutines(); // 이전 애니메이션 중지
        StartCoroutine(ScaleReturn(button)); // 크기 복귀 코루틴 시작
    }

    // 버튼을 일정 시간 동안 확대하는 코루틴
    IEnumerator ScaleTo(GameObject button)
    {
        float duration = 0.1f; // 애니메이션 시간
        float elapsed = 0f;
        Vector3 start = button.transform.localScale;
        Vector3 arrive = start * scaleMultiplier;

        while (elapsed < duration)
        {
            button.transform.localScale = Vector3.Lerp(start, arrive, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        button.transform.localScale = arrive; // 최종 크기 적용
    }

    // 버튼을 원래 크기로 되돌리는 코루틴
    IEnumerator ScaleReturn(GameObject button)
    {
        float duration = 0.1f;
        float elapsed = 0f;
        Vector3 start = button.transform.localScale;

        while (elapsed < duration)
        {
            button.transform.localScale = Vector3.Lerp(start, buttonDefaultScale, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        button.transform.localScale = buttonDefaultScale; // 원래 크기 복구
    }

    // 테스트용 함수 (버튼 작동 확인용)
    public void TestInterection()
    {
        Debug.Log("버튼이 제대로 누름");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
