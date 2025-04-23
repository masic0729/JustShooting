using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool isGameOver = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void GameOver()
    {
        SetIsGameOver(true);
    }
    //getset
    public void SetIsGameOver(bool state)
    {
        isGameOver = state;
    }

    public bool GetIsGameOver() => isGameOver;
}
