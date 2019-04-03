using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool pulled;
    private bool counted;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        pulled = false;
        counted = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getCounted()
    {
        if (counted)
        {
            //makes sure an instance is only counted once
            return false;
        }
        if (!counted&&pulled)
        {
            counted = true;
        }
        return counted;
    }

    //collision action
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pulling!");
        //pull switch
        if (this.gameObject != null&&pulled==false)
        {
            //animate
            anim.Play("SwitchPull");
            pulled = true;
        }
    }

}
