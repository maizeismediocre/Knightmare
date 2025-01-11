using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    // this script is based off the menu script from the marble game assignment.
    public InputAction startAction;

    public TMP_Text highscore_txt;
    public TMP_Text fastesttime_txt;

    void Start()
    {
        highscore_txt.text = "High Score: " + GameManager.Instance.highscore;
        fastesttime_txt.text = "Fastest Time: " + (int)GameManager.Instance.fastesttime;
        GameManager.Instance.totaltime = 0;
        
    }

    // when the play button is pressed load the next scene
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.instance.Play("Select");
    }
    // when the quit button is pressed quit the game
    public void QuitGame()
    {
        Application.Quit();

    }




}
