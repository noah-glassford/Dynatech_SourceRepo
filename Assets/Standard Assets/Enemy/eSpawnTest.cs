using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eSpawnTest : MonoBehaviour
{
    public GameObject enemey;
    public int ammount;

    private void Start()
    {
        for (int i = 0; i < ammount; i++) {
            Instantiate(enemey, transform.position, transform.rotation);
        }
    }
}
