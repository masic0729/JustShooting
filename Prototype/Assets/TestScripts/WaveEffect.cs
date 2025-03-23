using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        rectTransform.anchoredPosition = CalPlayerPowerOnUI();
    }

    
    Vector2 CalPlayerPowerOnUI()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float rectHeight = rectTransform.rect.height / 100;
        Vector2 instanceValue = new Vector2(0, player.powerStats.playerPower * rectHeight);
        return instanceValue;

    }
}
