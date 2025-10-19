using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    Player player;
    private RectTransform rectTransform;
    private Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

    }

    void Update()
    {
        rectTransform.anchoredPosition = CalPlayerPowerOnUI();
    }

    
    Vector2 CalPlayerPowerOnUI()
    {
        Vector2 instanceValue = Vector2.zero;
        if(player != null)
        {
            float rectWidth = rectTransform.rect.width / 50;
            instanceValue = new Vector2(-rectTransform.rect.width - 90 + player.powerStats.playerPower * rectWidth, 88);
        }
        return instanceValue;

    }
}