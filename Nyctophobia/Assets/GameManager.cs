using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Game manager for nyctophobia simulator
public class GameManager : MonoBehaviour
{

    //important game variables
    public float gameTimer; //time between beginning and completing a map 
    public float brightness; //current ambient light intensity 
    public float usage; //flashlight used 
    private bool win;//true if map completed 

    //Interactable Objects 
    public Switch switchObject; //switches to unlock exit 
    public Plug plugObject; //plugs to connect to activate power 
    public Door doorObject; //door blocking player from winning 
    public WinZone winObject; //player wins the game when contact is made

    //game update stuff
    private bool allPulled; //true if all switches have been pulled 
    private bool allPlugged; //true if all plugs are plugged
    public int switchCount;//total switches in map 
    private int switchesPulled;// current number of pulled switches
    public int plugCount;//total plugs in map 
    private int plugsPlugged;//current number of plugged plugs 

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        switchesPulled = 0;
        plugsPlugged = 0;
        allPulled = false;
        allPlugged = false;
    }
    //opens door if all switches pulled and all plugs plugged
    IEnumerator OpenDoor()
    {
        doorObject.Open();
        yield return null;
    }
    IEnumerator GameEnd()
    {
        while (win)
        {
            yield return new WaitForSeconds(2);
            if (win)
            {
                Debug.Log("Win!");
            }
            yield return null;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        //updating counts
        if (switchObject.getCounted() == false)
        {
            switchesPulled++;
        }
        if (plugObject.getCounted() == false)
        {
            plugsPlugged++;
        }
        //updating switches and plugs
        if (switchesPulled >= switchCount)
        {
            allPulled = true;
        }
        if(plugsPlugged >= plugCount)
        {
            allPlugged = true; 
        }
        //updating door
        if (allPulled&&allPlugged)
        {
            StartCoroutine(OpenDoor());
        }

        //updating win
        if (winObject.getWin())
        {
            win = true;
            StartCoroutine(GameEnd());
        }
    }
}
