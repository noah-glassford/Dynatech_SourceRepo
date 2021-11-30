using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgradeData : MonoBehaviour
{
    [Header("Version 2")]
    public int priceToUpgrade2 = 0;

    public int damagePerBullet2 = 20;
    public float fireRate2 = 0.25f;
    public int magazineSize2 = 8;

    public float heavyDamageMulti2 = 2.5f;
    public float midDamageMulti2 = 1.0f;
    public float lowDamageMulti2 = 0.75f;

    [Range(0f, 1f)] public float spreadFactor2 = 0.5f;

    public bool becomeBurst = false;
    public float burstRate2 = 0.05f;
    public bool becomeShotgun = false;
    public int shellCount2 = 10;

    [Header("Version 3")]
    public int priceToUpgrade3 = 0;

    public int damagePerBullet3 = 20;
    public float fireRate3 = 0.25f;
    public int magazineSize3 = 8;

    public float heavyDamageMulti3 = 2.5f;
    public float midDamageMulti3 = 1.0f;
    public float lowDamageMulti3 = 0.75f;

    [Range(0f, 1f)] public float spreadFactor3 = 0.5f;

    public float burstRate3 = 0.05f;
    public int shellCount3 = 10;

    [Header("Version 4")]
    public int priceToUpgrade4 = 0;

    public int damagePerBullet4 = 20;
    public float fireRate4 = 0.25f;
    public int magazineSize4 = 8;

    public float heavyDamageMulti4 = 2.5f;
    public float midDamageMulti4 = 1.0f;
    public float lowDamageMulti4 = 0.75f;

    [Range(0f, 1f)] public float spreadFactor4 = 0.5f;

    public float burstRate4 = 0.05f;
    public int shellCount4 = 10;

    [Header("Version 5")]
    public int priceToUpgrade5 = 0;

    public int damagePerBullet5 = 20;
    public float fireRate5 = 0.25f;
    public int magazineSize5 = 8;

    public float heavyDamageMulti5 = 2.5f;
    public float midDamageMulti5 = 1.0f;
    public float lowDamageMulti5 = 0.75f;

    [Range(0f, 1f)] public float spreadFactor5 = 0.5f;

    public float burstRate5 = 0.05f;
    public int shellCount5 = 10;

    public void NextUpgrade(PlayerGameData pgd) {

        Gun g = pgd.gunM.currentGun;
        if (g.gunLevel != 5)
        {
            g.gunLevel++;

            switch (g.gunLevel)
            {
                case 2:
                    g.damagePerBullet = damagePerBullet2;
                    g.fireRate = fireRate2;
                    g.magazineSize = magazineSize2;

                    g.spreadFactor = spreadFactor2;

                    if (becomeBurst) g.isBurst = true;
                    if (becomeShotgun) g.isShotgun = true;

                    g.burstRate = burstRate2;
                    g.shellCount = shellCount2;
                    break;
                case 3:
                    g.damagePerBullet = damagePerBullet3;
                    g.fireRate = fireRate3;
                    g.magazineSize = magazineSize3;

                    g.spreadFactor = spreadFactor3;

                    g.burstRate = burstRate3;
                    g.shellCount = shellCount3;
                    break;
                case 4:
                    g.damagePerBullet = damagePerBullet4;
                    g.fireRate = fireRate4;
                    g.magazineSize = magazineSize4;

                    g.spreadFactor = spreadFactor4;

                    g.burstRate = burstRate4;
                    g.shellCount = shellCount4;
                    break;
                case 5:
                    g.damagePerBullet = damagePerBullet5;
                    g.fireRate = fireRate5;
                    g.magazineSize = magazineSize5;

                    g.spreadFactor = spreadFactor5;

                    g.burstRate = burstRate5;
                    g.shellCount = shellCount5;
                    break;
            }

            g.spreadFactor = g.spreadFactor * 0.1f;
            g.roundsRemaining = g.magazineSize;
        }
    }
}
