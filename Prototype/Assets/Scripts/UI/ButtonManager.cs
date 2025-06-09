using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// UI ��ư�� ���õ� ����� ó���ϴ� Ŭ����
public class ButtonManager : MonoBehaviour
{
    private GameObject ClickedButton; // ����ڰ� Ŭ���� ��ư�� �����ϴ� ����

    public float scaleMultiplier = 1.1f; // ���콺 ���� �� ��ư ũ�� Ȯ�� ����
    private Vector3 buttonDefaultScale = new Vector3(1, 1, 1); // ��ư ���� ũ��

    // �� ��ȯ ó��
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // �� �ε�
        Time.timeScale = 1.0f; // �ð� �ٽ� �帣�� ����
    }

    // UI ��Ȱ��ȭ (��: �޴� �ݱ�)
    public void HideUI(GameObject target)
    {
        target.SetActive(false); // ������Ʈ ��Ȱ��ȭ
        Time.timeScale = 1.0f; // �ð� �ٽ� �帣�� ����
    }

    // UI Ȱ��ȭ (��: �޴� ����)
    public void ShowUI(GameObject target)
    {
        target.SetActive(true); // ������Ʈ Ȱ��ȭ
        Time.timeScale = 0.0f; // �ð� ���� (�Ͻ����� ȿ��)
    }

    // ��ư Ŭ�� �� ȿ���� ���
    public void ButtonSoundOutPut(string soundName)
    {
        if (soundName == "")
        {
            soundName = "ClickSample"; // �⺻ Ŭ�� ����
        }
        AudioManager.Instance.PlayINF(soundName); // ȿ���� ���
    }

    // ����� ����
    public void ChangeBGM(string soundName)
    {
        AudioManager.Instance.PlayBGM(soundName); // ���ο� ����� ���
    }

    // ��ư �ؽ�Ʈ�� �ٸ� �ؽ�Ʈ�� ����
    public void TransText(GameObject target)
    {
        // Ŭ���� ��ư�� �ؽ�Ʈ�� ��� �ؽ�Ʈ�� ����
        target.GetComponent<TextMeshProUGUI>().text = ClickedButton.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    // Ŭ���� ��ư ���� ����
    public void ClickButtonData(GameObject button)
    {
        ClickedButton = button;
    }

    // ���콺�� ��ư ���� �ö��� �� ũ�� Ű��� ȿ�� ����
    public void OnButtonEnter(GameObject button)
    {
        StopAllCoroutines(); // ���� �ִϸ��̼� ����
        StartCoroutine(ScaleTo(button)); // ũ�� Ȯ�� �ڷ�ƾ ����
    }

    // ���콺�� ��ư���� ����� �� ���� ũ��� ����
    public void OnButtonExit(GameObject button)
    {
        StopAllCoroutines(); // ���� �ִϸ��̼� ����
        StartCoroutine(ScaleReturn(button)); // ũ�� ���� �ڷ�ƾ ����
    }

    // ��ư�� ���� �ð� ���� Ȯ���ϴ� �ڷ�ƾ
    IEnumerator ScaleTo(GameObject button)
    {
        float duration = 0.1f; // �ִϸ��̼� �ð�
        float elapsed = 0f;
        Vector3 start = button.transform.localScale;
        Vector3 arrive = start * scaleMultiplier;

        while (elapsed < duration)
        {
            button.transform.localScale = Vector3.Lerp(start, arrive, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        button.transform.localScale = arrive; // ���� ũ�� ����
    }

    // ��ư�� ���� ũ��� �ǵ����� �ڷ�ƾ
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

        button.transform.localScale = buttonDefaultScale; // ���� ũ�� ����
    }

    // �׽�Ʈ�� �Լ� (��ư �۵� Ȯ�ο�)
    public void TestInterection()
    {
        Debug.Log("��ư�� ����� ����");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
