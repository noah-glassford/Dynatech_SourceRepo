using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerGameData pgd;

    public float intDis;
    public Text onIntTxt;
    public Camera pCam;

    private void Start()
    {
        pgd = this.GetComponent<PlayerGameData>();
        onIntTxt.text = " ";
    }

    private void Update()
    {

        Ray ray = pCam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, intDis)) {
            Interactablle inter = hit.collider.GetComponent<Interactablle>();

            if (inter != null)
            {
                HandleInteraction(inter);
                onIntTxt.text = inter.getDescription();
            }
            else onIntTxt.text = " ";
        }
        else onIntTxt.text = " ";

    }

    private void HandleInteraction(Interactablle inter) {

        KeyCode key = KeyCode.E;

        if (Input.GetKeyDown(key)) {
            inter.Interact(pgd);
        }

    }

}
