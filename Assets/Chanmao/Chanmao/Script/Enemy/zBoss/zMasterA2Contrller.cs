using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zMasterA2Contrller : MonoBehaviour
{
    public zEarthColler coller;
    // Start is called before the first frame update
    public ParticleSystem A2;
    // Update is called once per frame
    public void Play()
    {
        A2.Play();
        coller.Init();
    }
}
