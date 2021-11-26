using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuPrefs : MonoBehaviour
{
    //This script keeps track of the player prefs

    [SerializeField]
    private Slider MouseSensSlider;

    public float MouseSens;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("MouseSens"))
        {
            PlayerPrefs.SetFloat("MouseSens", 1f); //defaults sens to 1 if no value is set
        }

        MouseSens = PlayerPrefs.GetFloat("MouseSens");
    }

    // Update is called once per frame
    void Update()
    {
        
        MouseSensSlider.value = MouseSens;
    }

    public void SetMouseSensitivity()
    {
        MouseSens = MouseSensSlider.value;
        PlayerPrefs.SetFloat("MouseSens", MouseSens);
    }
}
