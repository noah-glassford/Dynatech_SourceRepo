using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactablle : MonoBehaviour
{

    public abstract string getDescription(PlayerGameData p);
    public abstract void Interact(PlayerGameData p);

}
