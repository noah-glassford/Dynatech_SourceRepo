using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JSAM;

public class gunManager : MonoBehaviour
{
    [Header("Gun Data")]
    public GameObject[] guns;
    public GameObject startingGun;

    [Header("Logical Data")]

    public Camera playerCamera;
    public GameObject player;
    public GameObject headJoint;
    public Transform gunPosition;


    [Header("UI Data")]
    public Text ammoDisplay;

    public Gun currentGun;

    [Header("Debug Data")]
    public int index = 0;

    bool isSwitching;

    private void Start()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons() 
    {
        addNewGun(startingGun, 0);
        addNewGun(startingGun, 1);
        switchWeapons(0);
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && !isSwitching) {
            index++;
            if (index > guns.Length-1) index = 0;
            StartCoroutine(SwitchAfterDelay(index));
        }
        ammoDisplay.text = currentGun.roundsRemaining + "/" + currentGun.magazineSize;
        //JSAM.AudioManager.PlaySound(Sounds.PISTOL);
    }

    public void addNewGun(GameObject gunHolder, int indexPos) {

        if (guns[indexPos] != null) {
            Destroy(currentGun.gameObject);
            guns[indexPos] = null;
        }
        GameObject g = Instantiate(gunHolder, gunPosition.position, gunPosition.rotation);
        currentGun = g.GetComponentInChildren<Gun>();
        g.transform.parent = gunPosition;
        Gun t = g.GetComponentInChildren<Gun>();
        t.playerCamera = playerCamera;
        t.player = player;
        t.headJoint = headJoint;

        guns[indexPos] = g;
        Debug.Log("Switching Weapons Started");
        switchWeapons(indexPos);
        
    }

    private IEnumerator SwitchAfterDelay(int _index) {

        isSwitching = true;
        yield return new WaitForSeconds(0.0f);
        switchWeapons(_index);

        isSwitching = false;
    }

    private void switchWeapons(int newIndex) 
    {
        Debug.Log("Switching Weapons Opened");
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i] != null)
                guns[i].SetActive(false);
        }
        guns[newIndex].SetActive(true);
        Debug.Log("Guns Switched");
        currentGun = guns[newIndex].GetComponentInChildren<Gun>();
        ammoDisplay.text = currentGun.roundsRemaining + "/" + currentGun.magazineSize;
    }
}
