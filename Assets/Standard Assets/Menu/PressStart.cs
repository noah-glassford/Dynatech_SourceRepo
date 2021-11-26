using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class PressStart : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] Animator startText;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Submit") && startText.GetCurrentAnimatorStateInfo(0).IsName("PressStart"))
        {
            startPress();
        }
    }

    void startPress()
    {
        menu.SetActive(true);
        this.gameObject.SetActive(false);
        JSAM.AudioManager.PlaySound(Sounds.UICONFIRM);
    }    
}
