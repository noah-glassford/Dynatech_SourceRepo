using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnPoint : MonoBehaviour
{
    public bool isDefault = false;

    public bool isActive = false;

    public void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        if (isDefault) isActive = true;
    }

    public void SpawnEnemies(GameObject enemy, int enemyHP, float enemySpeed, int enemyToSpawn) 
    {
        for (int i = 0; i < enemyToSpawn; i++)
        {
            GameObject tempE = EnemyObjectPool.instance.SpawnFromPool("Enemy", transform.position, transform.rotation);  //Instantiate(enemy, transform.position, transform.rotation);
            tempE.GetComponent<NavMeshAgent>().speed = enemySpeed * Random.Range(0.9f, 1.1f);
            Enemy e = tempE.GetComponent<Enemy>();
            e.Health = enemyHP;
        }
        StartCoroutine(SetOff());

    }

    IEnumerator SetOff() {
        isActive = false;
        yield return new WaitForSeconds(1f);
        isActive = true;
    }

}
