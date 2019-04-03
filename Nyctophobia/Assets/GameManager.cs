using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Game manager for nyctophobia simulator
public class GameManager : MonoBehaviour
{

    //arrays
    public GameObject[] switches;
    public GameObject[] plugs;

    //important game variables
    //performance tracking
    public float gameTimer; //time between beginning and completing a map  
    public float usage; //time spent using flashlight 
    public float batteries; //number of batteries collected 

    //therapist control variables 
    public Slider brightness; //current ambient light intensity
    public Slider decay; //rate at which flashlight brightness decays 
    public Slider beam; //width of flashlight beam 
    public Slider raycast; //maximum teleport distance 

    private bool win;//true if map completed 

    //Interactable Objects 
    public Switch switchObject; //switches to unlock exit 
    public Plug plugObject; //plugs to connect to activate power 
    public Door doorObject; //door blocking player from winning 
    public WinZone winObject; //player wins the game when contact is made

    //game update stuff
    private bool open;
    private bool allPulled; //true if all switches have been pulled 
    private bool allPlugged; //true if all plugs are plugged
    private int switchCount;//total switches in map 
    private int switchesPulled;// current number of pulled switches
    private int plugCount;//total plugs in map 
    private int plugsPlugged;//current number of plugged plugs 

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        switchesPulled = 0;
        plugsPlugged = 0;
        switchCount = switches.Length;
        plugCount = plugs.Length;
        allPulled = false;
        allPlugged = false;
        open = false;
        switches = GameObject.FindGameObjectsWithTag("Switch");
        plugs = GameObject.FindGameObjectsWithTag("Plug");
        RenderSettings.ambientIntensity = brightness.value;
    }
    //opens door if all switches pulled and all plugs plugged
    IEnumerator OpenDoor()
    {
        open = false;
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
    IEnumerator updateSwitches()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            Switch s = switches[i].GetComponent<Switch>();
            if (s.getCounted())
            {
                switchesPulled++;
            }
        }
        //updating switches 
        if (switchesPulled >= switchCount)
        {
            allPulled = true;
        }
        yield return null;
    }
    IEnumerator updatePlugs()
    { 
        for (int i = 0; i<plugs.Length; i++)
        {
            Plug p = plugs[i].GetComponent<Plug>();
            if (p.getCounted())
            {
                plugsPlugged++;
                Debug.Log("plugsPlugged: " + plugsPlugged);
            }
        }
        //updating plugs
        if (plugsPlugged >= plugCount)
        {
            allPlugged = true;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.ambientIntensity = brightness.value;

        //updating switches and plugs 
        StartCoroutine(updateSwitches());
        StartCoroutine(updatePlugs());

        //updating door
        if (allPulled&&allPlugged&&!open)
        {
            Debug.Log("Open the gates!");
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
