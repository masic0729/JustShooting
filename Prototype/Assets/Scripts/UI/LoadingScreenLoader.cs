using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    static bool isFirstClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadingLoad()
    {
        if(isFirstClicked == false)
        {
            isFirstClicked = true;
            loadingScreen.SetActive(true);
        }
    }
}
