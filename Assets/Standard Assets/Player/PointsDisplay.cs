using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    //public GameObject player;

    public GameObject[] players;

    [Range(1,4)]public int playerNumber;

    private bool hasStartup = false;

    public List<int> pointsToDisplay;
    public GameManager gm;
    public List<PlayerGameData> pd;
    private Text t;

    private void Start()
    {
        for(int i = 0; i < 4; i++) 
            pd.Add(null);
    }

    private void Update()
    {
        if (!hasStartup) {
            startUp();
        }

        for (int i = 0; i < gm.playerCount; i++)
        {
            if (pointsToDisplay[i] != pd[i].currentPoints && playerNumber == pd[i].playerNumber)
            //if (pointsToDisplay[i] != gd[i].currentPoints)
            {
                pointsToDisplay[i] = pd[i].currentPoints;
                t.text = pointsToDisplay[i].ToString();
            }
        }
    }

    private void startUp() {
        hasStartup = true;

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < gm.playerCount; i++)
        {
            pd[i] = players[i].GetComponent<PlayerGameData>();
            pointsToDisplay.Add(0);
            t = GetComponent<Text>();
            if (playerNumber <= gm.playerCount)
                t.text = "0";
            else t.text = " ";
        }
    }
}
