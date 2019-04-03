using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Open()
    {
        StartCoroutine(Activate());
    }
    IEnumerator Activate()
    {
        //animate
        anim.Play("DoorOpen");
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
