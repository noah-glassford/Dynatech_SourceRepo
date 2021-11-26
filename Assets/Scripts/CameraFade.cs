using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }
    void FadeOut()
    {
        this.GetComponent<Image>().CrossFadeAlpha(0, 5.0f, false);
    }
    void FadeIn()
    {
        this.GetComponent<Image>().CrossFadeAlpha(1, 5.0f, false);
    }
}
