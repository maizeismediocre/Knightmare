using UnityEngine;

public class Changelevel : MonoBehaviour
{
    //this script is based off one in the Scifi game assignment
    private void OnTriggerEnter(Collider other)
    {
        // if the player collides with the end zone use te goalreached method from the GamePlay script
        if (other.gameObject.tag == "Player")
        {

            GamePlay.Instance.goalreached();
        }
    }
}
