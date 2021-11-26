using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_dogo : MonoBehaviour
{
    private Enemy eScript;
    private void Start()
    {
        eScript = GetComponent<Enemy>();

        eScript.Health = (int)(eScript.Health * 0.5f);
        eScript.nav.speed += 1.2f;

        eScript.minDamage = 1;
        eScript.maxDamage = 2;
    }
}
