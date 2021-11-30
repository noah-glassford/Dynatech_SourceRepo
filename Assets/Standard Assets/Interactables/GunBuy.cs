using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class GunBuy : Interactablle
{
    public GameObject gun;

    public string gunToBuy = "Default Gun Name";
    public int gunPrice = 5000;

    public override string getDescription(PlayerGameData pgd)
    {
        Gun g = gun.GetComponentInChildren<Gun>();
        GunUpgradeData gUps = g.gameObject.GetComponent<GunUpgradeData>();

        if (pgd.gunM.currentGun.GunNumber == g.GunNumber) {
            switch (g.gunLevel) {
                case 1:
                    return "Press [E] To Upgrade " + gunToBuy + "-MK1 For " + gUps.priceToUpgrade2 + " Gears";
                case 2:
                    return "Press [E] To Upgrade " + gunToBuy + "-MK2 For " + gUps.priceToUpgrade3 + " Gears";
                case 3:
                    return "Press [E] To Upgrade " + gunToBuy + "-MK3 For " + gUps.priceToUpgrade4 + " Gears";
                case 4:
                    return "Press [E] To Upgrade " + gunToBuy + "-MK4 For " + gUps.priceToUpgrade5 + " Gears";
                case 5:
                    return "This Gun Is At Its Best Upgrade";
            }
        }
        return "Press [E] To Purchase " + gunToBuy + " For " + gunPrice.ToString() + " Gears";
    }
    public override void Interact(PlayerGameData pgd)
    {
        //if Gun Upgrade
        Gun g = gun.GetComponentInChildren<Gun>();
        GunUpgradeData gUps = g.gameObject.GetComponent<GunUpgradeData>();

        int gLevel = g.gunLevel;

            if (pgd.gunM.currentGun.GunNumber == g.GunNumber && pgd.currentPoints >= gUps.priceToUpgrade2 && gLevel == 1)
            {

                pgd.currentPoints -= gUps.priceToUpgrade2;
                JSAM.AudioManager.PlaySound(Sounds.WEAPONUPGRADE);
                gUps.NextUpgrade(pgd);
                pgd.gunM.gunLvl.text = pgd.gunM.currentGun.gunLevel.ToString();
            }
        else if (pgd.gunM.currentGun.GunNumber == g.GunNumber && pgd.currentPoints >= gUps.priceToUpgrade3 && gLevel == 2)
        {

            pgd.currentPoints -= gUps.priceToUpgrade3;
            JSAM.AudioManager.PlaySound(Sounds.WEAPONUPGRADE);
            gUps.NextUpgrade(pgd);
            pgd.gunM.gunLvl.text = pgd.gunM.currentGun.gunLevel.ToString();
        }
        else if (pgd.gunM.currentGun.GunNumber == g.GunNumber && pgd.currentPoints >= gUps.priceToUpgrade4 && gLevel == 3)
        {

            pgd.currentPoints -= gUps.priceToUpgrade4;
            JSAM.AudioManager.PlaySound(Sounds.WEAPONUPGRADE);
            gUps.NextUpgrade(pgd);
            pgd.gunM.gunLvl.text = pgd.gunM.currentGun.gunLevel.ToString();
        }
        else if (pgd.gunM.currentGun.GunNumber == g.GunNumber && pgd.currentPoints >= gUps.priceToUpgrade5 && gLevel == 4)
        {

            pgd.currentPoints -= gUps.priceToUpgrade5;
            JSAM.AudioManager.PlaySound(Sounds.WEAPONUPGRADE);
            gUps.NextUpgrade(pgd);
            pgd.gunM.gunLvl.text = pgd.gunM.currentGun.gunLevel.ToString();
        }
        //If Gun Buy
        else if (pgd.currentPoints >= gunPrice && pgd.gunM.currentGun.GunNumber != g.GunNumber)
        {
            pgd.currentPoints -= gunPrice;

            pgd.gunM.addNewGun(gun, pgd.gunM.index);

            JSAM.AudioManager.PlaySound(Sounds.WEAPONBUY);
        }
    }
}
