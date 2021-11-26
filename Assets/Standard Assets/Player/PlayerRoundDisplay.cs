using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JSAM;

public class PlayerRoundDisplay : MonoBehaviour
{
    private GameManager gm;
    private Text t;
    private int currentRound;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        t = GetComponent<Text>();
        UpdateText();
    }

    void Update()
    {
        if (currentRound != gm.currentRound)
            UpdateText();
    }

    private void UpdateText() {
        currentRound = gm.currentRound;
        t.text = "Wave " + currentRound.ToString();
        JSAM.AudioManager.PlaySound(Sounds.NEXTWAVE);
    }
}
