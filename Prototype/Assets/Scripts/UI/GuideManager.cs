using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    int index = 1;
    public GameObject[] guides;

    private void OnEnable()
    {
        for (int i = 1; i < guides.Length; i++)
        {
            guides[i].SetActive(false);
        }
        Time.timeScale = 0.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextGuide()
    {
        guides[index - 1].SetActive(false);
        guides[index].SetActive(true);
        index++;
        if(index == 3)
        {
            this.gameObject.SetActive(false);
        }
    }

}
