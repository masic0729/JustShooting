using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{
    private GameObject ClickedButton;

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
    
}
