using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    instance = managerObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private int collectedCoins = 0;
    private int collectedRedCoins = 0;
    private bool isSpeedUnlocked = false;
    private bool isDoubleJumpUnlocked = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //GameObject.FindGameObjectWithTag("Player").transform.position = lastcheckpointpos;
    }

    public int GetCollectedCoins()
    {
        return collectedCoins;
    }

    public void AddCoins(int amount)
    {
        collectedCoins += amount;
    }

    public int GetCollectedRedCoins()
    {
        return collectedRedCoins;
    }

    public void AddRedCoins(int amount)
    {
        collectedRedCoins += amount;
    }

    public bool IsSpeedUnlocked()
    {
        return isSpeedUnlocked;
    }

    public void SetSpeedUnlocked(bool unlocked)
    {
        isSpeedUnlocked = unlocked;
    }

    public bool IsDoubleJumpUnlocked()
    {
        return isDoubleJumpUnlocked;
    }

    public void SetDoubleJumpUnlocked(bool unlocked)
    {
        isDoubleJumpUnlocked = unlocked;
    }

    public void ResetData()
    {
        collectedCoins = 0;
        collectedRedCoins = 0;
        isSpeedUnlocked = false;
        isDoubleJumpUnlocked = false;
    }

    public void ResetCoinsForScene(string sceneName)
    {
        // Reset coin data for the specified scene
        if (sceneName == "mars")
        {
            // Reset coin data for scene 1
            collectedCoins = 0;
            collectedRedCoins= 0;
        }
        else if (sceneName == "moon")
        {
            // Reset coin data for scene 2
            collectedCoins = 0;
            collectedRedCoins= 0;
        }
        else if (sceneName == "jupiter")
        {
            // Reset coin data for scene 3
            collectedCoins = 0;
            collectedRedCoins= 0;
        }
    }
}
