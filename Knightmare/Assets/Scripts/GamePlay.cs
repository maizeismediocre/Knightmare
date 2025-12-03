using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using StarterAssets;
using Microsoft.Win32.SafeHandles;

// Game manager logic
public class GamePlay : MonoBehaviour
{
    // this script is based off the sci fi game assignment with some modifications such as the addition of a pause feature, changing how the enemyattack() function works, adding a checkpoint system and adding several ui features such as menu buttons for when the game is paused.


    // declare text fields
    public TMP_Text keystxt;           // Text field for the score text
    public TMP_Text gameovertxt;        // Text field for game over text
    public TMP_Text healthtxt;          // Text field for the health text
    public TMP_Text scoretxt;           // Text field for the score text
    public TMP_Text timetxt;            // Text field for the time text
    public TMP_Text pausetxt;           // Text field for the pause text
    public TMP_Text leveltxt;           // Text field for the level text
    public TMP_Text highscoretxt;       // Text field for the highscore text
    public TMP_Text fastesttimetxt;     // Text field for the fastest time text
    // declare buttons 
    public Button restartButton;
    public Button menuButton;

    public int lives = 3;        // the player health
    public bool gameover = false;   // a boolean to indicate the game over state

    public bool lost = false;       // a boolean to indicate the game lost state
    public int totalkeys = 0;       // Total number of keys to collect
    public int keyscollected = 0;   // Number of keys collected so far
    public int score = 0;           // The player's score
    public float time = 0;          // The time taken to complete the level
    public bool isPaused = false;   // Is the game paused

    public InputAction keyAction;
    GameObject player;              // Reference to the player object
    FirstPersonController playerController;
    CharacterController characterController;
    StarterAssetsInputs playerInputs;
    public Vector3 playerSpawn;

    
    void OnEnable()
    {
        keyAction.Enable();
        keyAction.performed += OnKey;
    }

    
    void OnDisable()
    {
        keyAction.performed -= OnKey;
        keyAction.Disable();
    }

    
    public void OnKey(InputAction.CallbackContext ctx)
    {
        //toggle the ispaused variable
        if (gameover) return;
        isPaused = !isPaused;
        //if the game is paused, set the time scale to 0
        if (isPaused)
        {
            Time.timeScale = 0;
            pausetxt.text = "PAUSED";
            // show the restart and menu buttons
            restartButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            //play the pause sound
            AudioManager.instance.Play("Pause");

        }
        //if the game is not paused, set the time scale to 1
        else
        {
            Time.timeScale = 1;
            pausetxt.text = "";
            // hide the restart and menu buttons
            restartButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
            //play the resume sound
            AudioManager.instance.Play("Resume");
        }

    }

    // This is a C# property - the code below isn't using it
    // as it is accessing the private static instance directly.
    // Use this property from other classes.
    public static GamePlay Instance
    {
        get
        {
            return instance;
        }
    }

    // GameManager instance
    public static GamePlay instance = null;

    // Use this for initialization
    void Start()
    {
        
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        timetxt.gameObject.SetActive(true);
        pausetxt.text = "";
        gameovertxt.text = "";
        highscoretxt.text = "";
        fastesttimetxt.text = "";
        setHealthText(lives);
       
        // set the level text to the current scene
        leveltxt.text = "Level: " + SceneManager.GetActiveScene().buildIndex;
        // Get total keys
        totalkeys = GameObject.FindObjectsByType<PickupTrigger>(FindObjectsSortMode.None).Length;

        keystxt.text = "Keys: " + keyscollected + "/" + totalkeys;
        score = score + GameManager.Instance.currentscore;
        time = GameManager.Instance.totaltime;
        // unpause the game
        Time.timeScale = 1;
        // find the player object
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<FirstPersonController>();
            characterController = player.GetComponent<CharacterController>();
            playerInputs = player.GetComponent<StarterAssetsInputs>();
            setplayerspawn();
        }

        
        setScoreText(score);
    }

    // Init the game manager
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        
    }

    void Update()
    {
        
        // increment time
        time += Time.deltaTime;

        GameManager.Instance.totaltime += Time.deltaTime;
        setTimeText(time);
    }





    // pickup() should increase the key count but 1. This is called by the PickupTrigger script when
    // the player moves through a key
    

    public void pickup()
    {
        // Check that not game over
        if (gameover) return;

      

        // Add one to total number of keys collected
        keyscollected++;
        score = score + 100;
        
        // Update the text field to display the total number of collected keys in the form "Keys: ??"
        keystxt.text = "Keys: " + keyscollected + "/" + totalkeys;
        setScoreText(score);
        // play the key sound
        AudioManager.instance.Play("Collectable");




    }

    
   
    public void goalreached()
    {
        //check if theres any more scenes and then load the next scene 
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // add the score to the total score
            GameManager.Instance.currentscore = score;
            // add the time to the total time
            GameManager.Instance.totaltime = time;
            //play next level sound

            AudioManager.instance.Play("Next level");
        }
        else
        {
            setGameOver();
        }
        
    }

    // Is it game over
    public bool isGameOver()
    {
        return gameover;
    }

    // Set the game over state to true
    
    public void setGameOver()
    {
        // add the score to the total score
        GameManager.Instance.currentscore = score;
        // add the time to the total time
        GameManager.Instance.totaltime = time;
        // if score is greater than highscore, set highscore to score
        if (GameManager.Instance.currentscore > GameManager.Instance.highscore)
        {
            GameManager.Instance.highscore = GameManager.Instance.currentscore;
            highscoretxt.text = "NEW HIGHSCORE!";
        }
        // if time is less than best time, set fastest time to time
        if (GameManager.Instance.totaltime < GameManager.Instance.fastesttime || GameManager.Instance.fastesttime == 0 && !lost)
        {
            GameManager.Instance.fastesttime = GameManager.Instance.totaltime;
            fastesttimetxt.text = "NEW FASTEST TIME!";
        }

        // hide all the text game objects
        keystxt.text = "";
        healthtxt.text = "";
        scoretxt.text = "";
        timetxt.gameObject.SetActive(false);
        pausetxt.text = "";
        leveltxt.text = "";
        menuButton.gameObject.SetActive(true);
       
        gameover = true;
        if (lost == true)
        {
            gameovertxt.text = "GAME OVER!";
            //play the game over sound
            AudioManager.instance.Play("Game Over");
        }
        if (lost == false)
        {
            gameovertxt.text = "YOU ESCAPED!";
            //play the game won sound
            AudioManager.instance.Play("Game Won");
        }



        Time.timeScale = 0;
        



    }

    // Set the health text
    public void setHealthText(int health)
    {
        healthtxt.text = "lives: " + lives;
    }
    // set the score text
    public void setScoreText(int score)
    {
        scoretxt.text = "Score: " + score;
    }
    // set the time text
    public void setTimeText(float time)
    {
        timetxt.text = "Time: " + (int)time;
    }

    // Something has attacked the player
   
    public void EnemyAttack()
    {
        
        lives = lives - 1;
        score = score - 50;
        if (lives < 0) lives = 0;
        setHealthText(lives);
        setScoreText(score);
        respawn();
        //play the lose life sound
        AudioManager.instance.Play("Lose Life");
        if (lives <= 0)
        {
            lost = true;
            setGameOver();
        }
        
    }

    // Get the health of the player
    public int getHealth()
    {
        return lives;
    }

    
    public void resetScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        score = GameManager.Instance.currentscore;
        //play select sound
        AudioManager.instance.Play("Select");
    }

    
    public void GoToMenu()
    {
        
        SceneManager.LoadScene(0);
        // reset current score to 0
        GameManager.Instance.currentscore = 0;
        // play select sound
        AudioManager.instance.Play("Select");
    }
    public void setplayerspawn()
    {
        //set the players spawn to the players current position
        playerSpawn = player.transform.position;

    }
    public void respawn()
    {
        //if the player has lives left, respawn the player at the spawn point
        if (lives > 0)
        {
             if (playerController != null)
            {
                playerController.enabled = false;
            }
            if (playerInputs != null)
            {
                playerInputs.move = Vector2.zero;
                playerInputs.look = Vector2.zero;
                playerInputs.jump = false;
                playerInputs.sprint = false;
            }

            // Reset the player's velocity
            if (characterController != null)
            {
                characterController.enabled = false; // Disable CharacterController to directly set position
                player.transform.position = playerSpawn;
                characterController.enabled = true; // Re-enable CharacterController
            }
            else
            {
                player.transform.position = playerSpawn;
            }

            // Re-enable the movement script
            if (playerController != null)
            {
                playerController.enabled = true;
            }
            
        }

    }
}
