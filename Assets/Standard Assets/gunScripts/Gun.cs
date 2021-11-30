using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;
using UnityEngine.VFX;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("MUST HAVE")]
    public int GunNumber = 000;
    public int gunLevel = 1;

    [Header("General Data")]
    public Camera playerCamera;
    public GameObject player;
    public GameObject headJoint;

    [Header("Gun Shooting Data")]
    public bool isAutomatic = true;

    public bool isBurst = false;
    public float burstRate = 0.05f;
    private bool canBurst = true;

    public bool isShotgun = false;
    public int shellCount = 10;

    [Header("Gun Damage Data")]
    public int damagePerBullet = 20;
    [Range(0f,1f)]
    public float spreadFactor = 0.5f;

    public float heavyDamageMulti = 2.5f;
    public float midDamageMulti = 1.0f;
    public float lowDamageMulti = 0.75f;

    [Header("Gun Magazine Data")]
    public int magazineSize = 8;
    [HideInInspector]
    public int roundsRemaining;
    public bool isReloading;

    [Header("Gun Fire Rate Data")]
    public float fireRate = 0.25f;
    private float fireTimer;

    [Header("Gun Pierce Data")]
    public int pierceHealth = 3;

    [Header("Effect Data")]
    public GameObject MuzzleFlash;
    public GameObject sparksParticle;
    private GunDecals gDecs;
    public Color gDecsColor;

    [Header("UI Icon")]
    public Sprite gunIcon;

    [Header("Animation Data")]
    public Animator anim;

    [Header("Recoil Data")]
    public float recoilStrengthVertical;
    public float recoilStrengthHorizontal;
    private float RecoilUp;
    private float RecoilSide;
    private int NumberOfShotsFullAuto; //counts how many shots go off

    private float ResetLerpT = 0;
    private bool isReseting = false;
    private Quaternion RotXForReset = new Quaternion(1, 1, 1, 1);


    //For Gun Visuals
    public LineRenderer bulletTrail;

    [SerializeField] private Transform bulletExit;



    void Start()
    {
        gDecs = GetComponent<GunDecals>();
        gDecsColor = gDecs.secondaryColor;
        roundsRemaining = magazineSize;
        spreadFactor = spreadFactor * 0.1f;
    }
    private void Update()
    {
        //Automatic Inputs
        if (Input.GetButton("Fire1") && isAutomatic)
        {
            if (roundsRemaining > 0 && isBurst)
            {
                StartCoroutine(BurstFire());
            }
            else if (roundsRemaining > 0 && isShotgun)
            {
                ShotgunFire();
            }
            else if (roundsRemaining > 0 && !isBurst) {
                Fire();
            }
            else
            {
                DoReload();
            }
        }
        //Semi Auto Inputs
        else if (Input.GetButtonDown("Fire1")) {
            if (roundsRemaining > 0 && isBurst)
            {
                StartCoroutine(BurstFire());
            }
            else if (roundsRemaining > 0 && isShotgun)
            {
                ShotgunFire();
            }
            else if (roundsRemaining > 0 && !isBurst)
            {
                Fire();
            }
            else
            {
                DoReload();
            }
        }


        if (isReseting) //This block handles the recoil reset, needs to be in update since it uses LERP
        {
            ResetLerpT += Time.deltaTime * 7;

            headJoint.transform.localRotation = Quaternion.Lerp(RotXForReset, Quaternion.Euler(-1,0,0), ResetLerpT);

            if (ResetLerpT >= 1)
            {
                isReseting = false;
                ResetLerpT = 0f;
            }

        }

        //Reloading Inputs
        if (Input.GetKeyDown(KeyCode.R)) DoReload();

        if (fireTimer < fireRate) fireTimer += Time.deltaTime;

        //Aiming Sinputs
        AimDownSights();
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        isReloading = info.IsName("Reload");
    }

    private void AimDownSights()
    {
        if (Input.GetButton("Fire2") && !isReloading && playerCamera.fieldOfView > 60)
        {
            playerCamera.fieldOfView -= 6f;
        }
        if (!Input.GetButton("Fire2") && playerCamera.fieldOfView < 90 || isReloading && playerCamera.fieldOfView < 90)
        {
            playerCamera.fieldOfView += 6f;
        }
    }

    IEnumerator BurstFire() {
        if (canBurst && fireTimer >= fireRate && !isReloading)
        {
            canBurst = false;
            for (int i = 0; i < 3; i++)
            {
                Fire();
                yield return new WaitForSeconds(burstRate); // wait till the next round
            }
            fireTimer = 0.0f;
            canBurst = true;
        }
    }

    private void ShotgunFire() {
        if (fireTimer >= fireRate && !isReloading)
        {

            for (int i = 0; i < shellCount; i++)
            {
                Fire();
            }
            //Logistics
            roundsRemaining--;
            anim.CrossFadeInFixedTime("Fire", 0.1f);
            fireTimer = 0.0f;

            //Muzzle
            GameObject flashObj = Instantiate(MuzzleFlash, bulletExit.position, Quaternion.identity);
            flashObj.transform.parent = bulletExit;
            flashObj.transform.localRotation = Quaternion.Euler(0, -90, 0);
            Destroy(flashObj, 0.15f);
        }
    }

    private void Fire()
    {
        //Kickback if the gun is unable to shoot
        if (fireTimer < fireRate || roundsRemaining <= 0 || isReloading) return;
        else if (!isShotgun)
        {
            //spawn muzzle flash
            GameObject flashObj = Instantiate(MuzzleFlash, bulletExit.position, Quaternion.identity);
            flashObj.transform.parent = bulletExit;
            flashObj.transform.localRotation = Quaternion.Euler(0, -90, 0);
            Destroy(flashObj, 0.15f);

            if(!isBurst) fireTimer = 0.0f;
        }

        //RecoilUp -= recoilStrengthVertical;
        //RecoilSide = Random.Range(-recoilStrengthHorizontal, recoilStrengthHorizontal) / 5;


        //Bullet Spread
        Vector3 shootDir = playerCamera.transform.forward;
        shootDir.x += Random.Range(-spreadFactor, spreadFactor);
        shootDir.y += Random.Range(-spreadFactor, spreadFactor);

        //Pierce Bullet Code

        RaycastHit[] hit = Physics.RaycastAll(playerCamera.transform.position, shootDir, LayerMask.GetMask("Enemy"));

        //spawn bullet trail
        GameObject bulletTrailFX = Instantiate(bulletTrail.gameObject, bulletExit.position, Quaternion.identity);
        LineRenderer lineR = bulletTrailFX.GetComponent<LineRenderer>();
        lineR.SetPosition(0, bulletExit.position);
        lineR.SetPosition(1, bulletExit.position + (-bulletExit.transform.right * 100f));
        Destroy(bulletTrailFX, 0.25f);

        if (hit.Length > 0)
        {
            lineR.SetPosition(1, hit[0].point);
        }

        int tPierce = pierceHealth;
        foreach (RaycastHit r in hit)
        {
            if (r.transform.gameObject.tag == "EnemyHeavy" && tPierce > 0)
            {
                GameObject newParticle = Instantiate(sparksParticle, r.point, r.transform.rotation);
                GameObject.Destroy(newParticle, 0.3f);

                float damage = damagePerBullet * Random.Range(heavyDamageMulti - 0.1f, heavyDamageMulti + 0.1f);
                r.transform.gameObject.GetComponentInParent<Enemy>().TakeDamage((int)damage, player, gDecsColor);
                tPierce--;
            }
            else if (r.transform.gameObject.tag == "EnemyAverage" || r.transform.gameObject.tag == "Enemy" && tPierce > 0)
            {
                GameObject newParticle = Instantiate(sparksParticle, r.point, r.transform.rotation);
                GameObject.Destroy(newParticle, 0.3f);

                float damage = damagePerBullet * Random.Range(midDamageMulti - 0.1f, midDamageMulti + 0.1f);
                r.transform.gameObject.GetComponentInParent<Enemy>().TakeDamage((int)damage, player, gDecsColor);
                tPierce--;
            }
            else if (r.transform.gameObject.tag == "EnemyLow" && tPierce > 0)
            {
                GameObject newParticle = Instantiate(sparksParticle, r.point, r.transform.rotation);
                GameObject.Destroy(newParticle, 1f);

                float damage = damagePerBullet * Random.Range(lowDamageMulti - 0.1f, lowDamageMulti + 0.1f);
                r.transform.gameObject.GetComponentInParent<Enemy>().TakeDamage((int)damage, player, gDecsColor);
                tPierce--;
            }

            //Provide a force to the rigidbody of the enemies
            Collider[] colliders = Physics.OverlapSphere(r.transform.position, 1f);

            foreach (Collider closeObj in colliders) {
                Rigidbody rb = closeObj.GetComponent<Rigidbody>();

                if (rb != null) {
                    rb.AddExplosionForce(100f, r.transform.position, 1f);
                }
            }

        }

        if (!isShotgun)
        {
            //Gun Logic Triggers
            roundsRemaining--;

            //Animation Triggers
            anim.CrossFadeInFixedTime("Fire", 0.1f);
        }

     


    }

    private void DoReload()
    {
        fireTimer = 0.0f;

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (isReloading) return;
        if (roundsRemaining == magazineSize) return;
        anim.CrossFadeInFixedTime("Reload", 0.01f);

        Reload();
    }

    private void Reload()
    {
        roundsRemaining = magazineSize;
        JSAM.AudioManager.PlaySound(Sounds.RELOAD);
    }


    /*
    private void ResetRecoilSmooth()
    {

        RecoilUp = 0;
        RecoilSide = 0;
        isReseting = true;

        RotXForReset = headJoint.transform.localRotation;


    }
    
    private void ApplyRecoil()
    {

        Vector3 RotationInEuler = headJoint.transform.localRotation.eulerAngles;
        RotationInEuler.x += RecoilUp;
        RotationInEuler.y += RecoilSide;

        if (RotationInEuler.x < 335 )
        {
            RotationInEuler.x = 335;

        }

        headJoint.transform.localRotation = Quaternion.Euler(RotationInEuler);
      

    }
    */

}
