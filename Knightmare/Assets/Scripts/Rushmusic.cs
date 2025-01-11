using UnityEngine;
using TMPro;
public class Rushmusic : MonoBehaviour
{
    //this script is orignal and was created by me
    public TMP_Text Runtxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Runtxt.text = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the trigger, play the rush music
        if (other.gameObject.tag == "Player")
        {
            // Find the AudioManager object and stop the theme and play this instead
            AudioManager.instance.Pause("Theme");
            AudioManager.instance.Play("Rush music");
            Runtxt.text = "RUN!!!";

        }


    }
    //when the player exits the trigger hide the run text
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Runtxt.text = "";
        }
    }
    private void OnDisable()
    {
        // Change the music back to the theme
        

        AudioManager.instance.Stop("Rush music");
        AudioManager.instance.UnPause("Theme");
        // destroy the audio source
       
    }
}


