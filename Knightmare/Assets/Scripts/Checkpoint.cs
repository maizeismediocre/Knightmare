using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //this script is original and was created by me
    public TMP_Text checkpointtxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        checkpointtxt.text = "";

    }
    public void OnTriggerEnter(Collider other)
    {
        // if the player collides with the checkpoint use the setplayerspawn method from the GamePlay script
        if (other.gameObject.tag == "Player")
        {
            checkpointtxt.text = "Checkpoint reached!";
            GamePlay.Instance.setplayerspawn();
            //play checkpoint sound
            AudioManager.instance.Play("Checkpoint");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        //reset the checkpoint text
        if (other.gameObject.tag == "Player")
        {
            checkpointtxt.text = "";
        }
    }
}
