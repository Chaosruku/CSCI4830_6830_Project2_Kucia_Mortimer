using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private bool win;
    // Start is called before the first frame update
    void Start()
    {
        win = false;
    }

    public bool getWin()
    {
        return win;
    }
    // Update is called once per frame
    void Update()
    {

    }
    //collision action
    private void OnCollisionEnter(Collision collision)
    {
        //player wins the game
        if (this.gameObject != null)
        {
            //animate
            win = true;
        }
    }
}
