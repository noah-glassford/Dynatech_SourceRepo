using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private bool canSpawn = true;

    public void spawnPlayer(GameObject p, GameManager gm, int playerCount, int playerNum) {
        if (canSpawn)
        {
            GameObject temp = Instantiate(p, transform.position, transform.rotation);
            temp.GetComponent<PlayerGameData>().setData(gm, playerCount, playerNum);
            Destroy();
        }
    }

    public void Destroy()
    {
        canSpawn = false;
        Destroy(gameObject);
    }
}
