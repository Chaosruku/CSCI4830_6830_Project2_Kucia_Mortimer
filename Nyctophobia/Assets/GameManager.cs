using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Game manager for nyctophobia simulator
public class GameManager : MonoBehaviour
{

    //arrays
    public GameObject[] switches;
    public GameObject[] plugs;

    //important game variables
    //performance tracking
    private float gameTimer; //time between beginning and completing a map  
    private float usage; //time spent using flashlight 

    //therapist control variables 
    public Slider brightness; //current ambient light intensity
    public Slider decay; //rate at which flashlight brightness decays 
    public Slider beam; //width of flashlight beam 
    public Slider fIntensity; //maximum teleport distance 

    private bool win;//true if map completed 

    //Interactable Objects 
    public Light flashlight; //player flashlight
    public FlashlightControl fcontrol; //FlashlightController holds timer
    public Switch switchObject; //switches to unlock exit 
    public Plug plugObject; //plugs to connect to activate power 
    public Door doorObject; //door blocking player from winning 
    public WinZone winObject; //player wins the game when contact is made

    //game update stuff
    private float initialSpot; //initial spot angle of flashlight
    private float initialIntensity; //initial y value of teleport laser 
    private bool open; //door may or may not be open
    private bool allPulled; //true if all switches have been pulled 
    private bool allPlugged; //true if all plugs are plugged
    private int switchCount;//total switches in map 
    private int switchesPulled;// current number of pulled switches
    private int plugCount;//total plugs in map 
    private int plugsPlugged;//current number of plugged plugs 

    //timers
    private float startTime; //Time the scene starts
    private float currentTime; //tracks current time
    private float lightOld; //old flashlight usage 

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        switchesPulled = 0;
        plugsPlugged = 0;

        allPulled = false;
        allPlugged = false;
        open = false;
        switches = GameObject.FindGameObjectsWithTag("Switch");
        plugs = GameObject.FindGameObjectsWithTag("Plug");
        switchCount = switches.Length;
        plugCount = plugs.Length;
        //initializing therapist controls
        RenderSettings.ambientIntensity = brightness.value;
        initialSpot = flashlight.spotAngle;
        initialIntensity = flashlight.intensity;
        startTime = Time.time;
        currentTime = startTime;
        lightOld = fcontrol.totalTime;
        gameTimer = 0;
        usage = 0;
    }
    //opens door if all switches pulled and all plugs plugged
    IEnumerator OpenDoor()
    {
        open = true;
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
        for (int i = 0; i < plugs.Length; i++)
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
        //timer
        currentTime = Time.time;
        if (!win)
        {
            gameTimer = currentTime - startTime;
            usage = fcontrol.totalTime;
        }

        //therapist controls
        RenderSettings.ambientIntensity = brightness.value;
        if (fcontrol.totalTime-lightOld>=3000)
        {
            lightOld = fcontrol.totalTime;
            initialIntensity *= 1 / decay.value;
        }
        flashlight.spotAngle = initialSpot * beam.value;
        flashlight.intensity = initialIntensity * fIntensity.value;

        //updating switches and plugs 
        StartCoroutine(updateSwitches());
        StartCoroutine(updatePlugs());

        //updating door
        if (allPulled && allPlugged && !open)
        {
            Debug.Log("Open the gates!");
            StartCoroutine(OpenDoor());
        }

        //updating win
        if (winObject.getWin())
        {
            win = true;
            Debug.Log("Previous Completion Time: " + PlayerPrefs.GetFloat("GameTime", gameTimer));
            Debug.Log("Previous Flashlight Usage Time: " + PlayerPrefs.GetFloat("UsageTime", usage));
            PlayerPrefs.SetFloat("GameTime", gameTimer);
            PlayerPrefs.SetFloat("UsageTime", usage);
            StartCoroutine(GameEnd());
        }
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
