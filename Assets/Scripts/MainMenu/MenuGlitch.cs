using UnityEngine;
using System.Collections;
using NaughtyAttributes;

public class MenuGlitch : MonoBehaviour {

    public static MenuGlitch Instance;
    public float cd = 0.0f;
    public float cdSpeed = 0.1f;
    public float slj = 0.0f;
    public float sljSpeed = 0.11f;
    public float vj = 0.0f;
    public float vjSpeed = 0.1f;
    public float hs= 0.0f;
    public float hsSpeed = 0.1f;
    public float dgi = 0.0f;
    public float dgiSpeed = 0.1f;
    public bool active;
    
    void Awake()
    {
        Instance = this;
    }

	void Update () {
        GetComponent<Kino.AnalogGlitch>().colorDrift = cd;
        GetComponent<Kino.AnalogGlitch>().scanLineJitter = slj;
        GetComponent<Kino.AnalogGlitch>().verticalJump = vj;
        GetComponent<Kino.AnalogGlitch>().horizontalShake = hs;
        GetComponent<Kino.DigitalGlitch>().intensity = dgi;

        if (active)
        {
            cd = Mathf.MoveTowards(cd, 0.3f, Time.deltaTime * cdSpeed);
            slj = Mathf.MoveTowards(slj, 0.3f, Time.deltaTime * sljSpeed);
            vj = Mathf.MoveTowards(vj, 0.3f, Time.deltaTime * vjSpeed);
            hs = Mathf.MoveTowards(hs, 0.3f, Time.deltaTime * hsSpeed);
            dgi = Mathf.MoveTowards(dgi, 0.01f, Time.deltaTime * dgiSpeed);
        }
    }

    [Button("Reset")]
    public void Reset()
    {
        cd = 0;
        slj = 0;
        vj = 0;
        hs = 0;
        dgi = 0;
    }
}