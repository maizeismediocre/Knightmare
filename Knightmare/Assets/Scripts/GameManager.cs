
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this script is based off the game manager from the marbles game assignment
    public int highscore = 0;
    public float totaltime = 0;
    public int currentscore = 0;
    public float fastesttime = 0;
    // This is a C# property - the code below isn't using it
    // as it is accessing the private static instance directly.
    // Use this property from other classes.
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance = null;
    void Awake()
    {
        if (instance)
        {
            Debug.Log("already an instance so destroying new one");
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}


