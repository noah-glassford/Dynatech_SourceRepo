using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderFade : MonoBehaviour
{
    [SerializeField] private Color c;

    [SerializeField] private float s = 100f;

    LineRenderer lr;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        c.a = Mathf.Lerp(c.a, 0, Time.deltaTime * s);
        
        lr.startColor = c;
        lr.endColor = c;
    }
}
