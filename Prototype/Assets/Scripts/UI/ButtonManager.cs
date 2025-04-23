using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void HideUI(GameObject target)
    {
        target.SetActive(false);
    }

    public void ShowUI(GameObject target)
    {
        target.SetActive(true);
    }
}
