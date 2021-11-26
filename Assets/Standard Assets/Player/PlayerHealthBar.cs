using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    public PlayerGameData pgd;

    public Image healthBar;

    private float lerpSpeed;

    private void Update()
    {
        lerpSpeed = 2f * Time.deltaTime;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, pgd.health / 6f, lerpSpeed);

        Color healthColor = Color.Lerp(Color.red, Color.white, (pgd.health / 6f));
        healthBar.color = healthColor;
    }
}
