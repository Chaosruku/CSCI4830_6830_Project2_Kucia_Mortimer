using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FlashlightControl : MonoBehaviour
{
    public FlashlightLight bulb;
    Light bulblight;
    public SteamVR_Input_Sources mySource;
    public bool lightOn;
    public bool lightOnOld;
    public float onTime; //starts timer when flashlight is turned on 
    public float offTime; //ends timer when flashlight is turned off
    public float totalTime; //tracks total flashlight usage 

    // Start is called before the first frame update
    void Start()
    {
        bulblight = bulb.GetComponent<Light>();
        bulblight.enabled = false;
        lightOn = false;
        lightOnOld = lightOn;
        offTime = Time.time;
        onTime = Time.time;
        totalTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Input.GetStateDown("GrabPinch", mySource))
        {
            bulblight.enabled = !bulblight.enabled;
            
            lightOn = !lightOn;
            if (!lightOnOld && lightOn)
            {
                onTime = Time.time;
            }
            if (lightOnOld && !lightOn)
            {
                offTime = Time.time;
            }
            lightOnOld = lightOn;
        }
        if (lightOn && onTime > offTime)
        {
            totalTime += Time.time - onTime;
        }
        //else
        //{
        //    bulblight.enabled = false;
        //}
    }
}
