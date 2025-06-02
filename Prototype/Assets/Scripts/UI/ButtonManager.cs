using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{
    private GameObject ClickedButton;
    public float scaleMultiplier = 1.1f; // 마우스 올렸을 때 크기 비율
    private Vector3 buttonDefaultScale = new Vector3(1,1,1);
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1.0f;

    }

    public void HideUI(GameObject target)
    {
        target.SetActive(false);
        Time.timeScale = 1.0f;

    }

    public void ShowUI(GameObject target)
    {
        target.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ButtonSoundOutPut(string soundName)
    {
        if(soundName == "")
        {
            soundName = "ClickSample";
        }
        AudioManager.Instance.PlayINF(soundName);
    }

    public void ChangeBGM(string soundName)
    {
        AudioManager.Instance.PlayBGM(soundName);
    }

    public void TransText(GameObject target)
    {
        
        target.GetComponent<TextMeshProUGUI>().text = ClickedButton.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void ClickButtonData(GameObject button)
    {
        ClickedButton = button;
    }

    public void OnButtonEnter(GameObject button)
    {
        StopAllCoroutines();
        
        StartCoroutine(ScaleTo(button));
    }

    public void OnButtonExit(GameObject button)
    {
        StopAllCoroutines();
        //button.transform.localScale = buttonDefaultScale;
        StartCoroutine(ScaleReturn(button));
    }

    IEnumerator ScaleTo(GameObject button)
    {
        float duration = 0.1f;
        float elapsed = 0f;
        Vector3 start = button.transform.localScale;
        Vector3 arrive = start * scaleMultiplier;
        while (elapsed < duration)
        {
            button.transform.localScale = Vector3.Lerp(start, arrive, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        button.transform.localScale = arrive;
    }

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

        button.transform.localScale = buttonDefaultScale;
    }

    public void TestInterection()
    {
        Debug.Log("버튼이 제대로 누름");
    }
}
