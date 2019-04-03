using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
