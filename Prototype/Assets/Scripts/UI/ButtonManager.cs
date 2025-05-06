using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{


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
}
