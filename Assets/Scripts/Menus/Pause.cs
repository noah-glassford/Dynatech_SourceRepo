using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject PauseMenuObject;

    [SerializeField]
    GameObject OptionsScreenObject;
    [SerializeField]
    GameObject MainScreenObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenuObject.activeSelf == false)
            {
                OpenPause();
              
            }
            else
            {
                UnPause();
            }
        }
    }

    public void OpenPause()
    {
        PauseMenuObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        OptionsScreenObject.SetActive(false);
        MainScreenObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        PauseMenuObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        OptionsScreenObject.SetActive(false);
        MainScreenObject.SetActive(true);
        Time.timeScale = 1f;
    }
}
