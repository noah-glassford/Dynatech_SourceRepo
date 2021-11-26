using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class Explosive : MonoBehaviour
{
    public float FuseLength = 0.0f;
    public float Radius;
    public int Damage;

    public bool ExplodeOnImpact;

    public AudioPlayer tick;
    public AudioPlayer explode;

    public GameObject particlePrefab;

    private float Timer;

    private bool hasExploded = false;

    private void Start()
    {
        Timer = FuseLength;
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        if (Timer <=0f && !hasExploded)
        {
            Explode();

            hasExploded = true;
        }
    }

    void Explode()
    {
        //instantiate visuals here

        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (Collider nearbyObject in colliders)
        {

            if (nearbyObject.gameObject.tag == "Enemy" || nearbyObject.tag == "EnemyLow" || nearbyObject.tag == "EnemyAverage" || nearbyObject.tag == "EnemyHeavy")
            nearbyObject.GetComponentInParent<Enemy>().TakeDamage(Damage, GameObject.FindGameObjectWithTag("Player"), Color.green);
        }

        GameObject explosionVisual = Instantiate(particlePrefab, transform.position, transform.rotation);


        explode.PlaySound();

        Destroy(tick.gameObject);
        Destroy(explode.gameObject);

        Destroy(explosionVisual, 1f);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ExplodeOnImpact)
        {
            Explode();
        }
    }
}
