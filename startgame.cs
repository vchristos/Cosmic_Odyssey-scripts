using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class startgame : MonoBehaviour

{
    public string targetSceneName;
   
    void Start()
    {
       
        
    }

    public void startbutton () {

        SceneManager.LoadScene(targetSceneName);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1); 
     
   
}
    // Update is called once per frame
    void Update()
    {
        
    }

}