using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlicker : MonoBehaviour
{
    private Color c;

    public void FadeDamage()
    {
        c = GetComponent<Image>().color;
        c.a = 0.6f;

        GetComponent<Image>().color = c;
    }
    private void Update()
    {
        if (c.a > 0) {
            c.a -= 0.02f;
            GetComponent<Image>().color = c;
        }
    }
}
