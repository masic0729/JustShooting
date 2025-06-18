using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    public float rotateValue = 0;
    bool canKeyDown = true;
    


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && canKeyDown == true)
        {
            StopAllCoroutines();
            StartCoroutine(RotateCameraZ());
        }
    }

    IEnumerator RotateCameraZ()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        Quaternion startRotation = transform.rotation;
        if(rotateValue == 180f)
        {
            rotateValue = 0f;
        }
        else
        {
            rotateValue = 180f;
        }
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotateValue);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        transform.rotation = endRotation; // 정확히 끝 각도로 맞춤
    }
}
