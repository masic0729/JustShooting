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
            float rectHeight = rectTransform.rect.height / 100;
            instanceValue = new Vector2(0, player.powerStats.playerPower * rectHeight - rectTransform.rect.height);
        }
        return instanceValue;

    }
}