using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;
using UnityEngine.AI;

public class Door : Interactablle
{

    public bool isClosed = true;
    public int priceToOpen = 2500;

    public EnemySpawnPoint[] enemySpawnPointsToEnable;
    public Animator doorAnim;
    public Collider col;

    public override string getDescription(PlayerGameData pgd)
    {
        if (isClosed) return "Press [E] To Open Door For " + priceToOpen.ToString() + " Gears";
        else return "AHHHOHOOHOHOHOHOHOHO";
    }
    public override void Interact(PlayerGameData pgd)
    {
        if (pgd.currentPoints >= priceToOpen)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);
            
            pgd.currentPoints -= priceToOpen;
            isClosed = false;
            doorAnim.SetBool("DoorOpen", true);
            Destroy(col);
            Destroy(gameObject.GetComponent<NavMeshObstacle>());
            JSAM.AudioManager.PlaySound(Sounds.DOOR);


            foreach (EnemySpawnPoint e in enemySpawnPointsToEnable) {
                e.isActive = true;
            }
        }
    }
}
