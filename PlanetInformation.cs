using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInformation : MonoBehaviour
{

    public GameObject infoPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.transform.tag == "Player")
        {   
            Time.timeScale = 0f;
            infoPanel.SetActive(true);
        }    
    }

    public void Continue()
    {   
        Time.timeScale = 1f;
        infoPanel.SetActive(false);
    }       
}
