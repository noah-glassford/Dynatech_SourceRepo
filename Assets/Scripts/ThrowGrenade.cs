using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    [SerializeField]
    private GameObject GrenadePrefab;

    public float ThrowPower;

    [SerializeField]
    private Transform CameraTrans;

    public float ThrowCooldownTimer = 5f;

    private float InternalCooldown;

    private bool CanThrow = false;

    private void Throw()
    {
        Vector3 ThrowForce;
        ThrowForce = CameraTrans.forward.normalized;
        ThrowForce *= ThrowPower;
     
        Instantiate(GrenadePrefab, CameraTrans.transform.position + CameraTrans.transform.forward * 2, Quaternion.identity).GetComponentInChildren<Rigidbody>().AddForce(ThrowForce, ForceMode.Impulse);
        Debug.Log("Applied Force of: " + ThrowForce.ToString());

        CanThrow = false;
        InternalCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (CanThrow)
            Throw();
        }

        InternalCooldown += Time.deltaTime;

        if (InternalCooldown >= ThrowCooldownTimer)
            CanThrow = true;

    }
}
