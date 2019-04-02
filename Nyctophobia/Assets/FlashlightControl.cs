using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FlashlightControl : MonoBehaviour
{
    public FlashlightLight bulb;
    Light bulblight;
    public SteamVR_Input_Sources mySource;

    // Start is called before the first frame update
    void Start()
    {
        bulblight = bulb.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Input.GetStateDown("GrabPinch", mySource))
        {
            bulblight.enabled = true;
        }
        else
        {
            bulblight.enabled = false;
        }
    }
}
