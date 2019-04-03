using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    private bool plugged;
    private bool counted;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        plugged = false;
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
        if (!counted&&plugged)
        {
            counted = true;
        }
        return counted;
    }


    //collision action
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Plugging!");
        //pull switch
        if (this.gameObject != null&&plugged==false)
        {
            //animate
            anim.Play("PlugIn");
            plugged = true;
        }
    }
}
