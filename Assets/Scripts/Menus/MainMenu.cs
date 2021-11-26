using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;
public class MainMenu : MonoBehaviour
{
   public void SwitchToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void onHighlight()
    {
        JSAM.AudioManager.PlaySound(Sounds.UIHOVER);
    }
    public void onSelect()
    {
        JSAM.AudioManager.PlaySound(Sounds.UICONFIRM);
    }
}
