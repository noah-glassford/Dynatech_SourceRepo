using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDecals : MonoBehaviour
{
    public bool randomColor;
    public Color secondaryColor;

    [Header("Gun Attachments")]
    public GameObject[] attachments;

    public ParticleSystem[] particles;

    [Header("Gun Ligthing")]
    public Light[] lights;

    private void Awake()
    {
        if (randomColor) 
        {
            secondaryColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        //Apply Color to Attachments
        foreach (GameObject t in attachments) 
        {
            MeshRenderer temp = t.GetComponent<MeshRenderer>();
            temp.material.color = secondaryColor;
        }
        //Apply Color to Effects

        foreach (ParticleSystem t in particles)
        {
            var main = t.main;
            main.startColor = secondaryColor;
        }

        //Apply Color To Lights

        foreach (Light t in lights)
        {
            t.color = secondaryColor;
        }
    }
}
