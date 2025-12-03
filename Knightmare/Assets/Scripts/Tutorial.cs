using UnityEngine;
using TMPro;
public class Tutorial : MonoBehaviour
{
    public TMP_Text hinttxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // this script is original and was created by me
    void Start()
    {
        hinttxt.text = "";
    }

    
    //show controls text 
    private void OnTriggerEnter(Collider other)
    {
        //if the player enters the trigger area, display a hint to the player
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Controls")
            {
                hinttxt.text = "Use the W, A, S, D keys to move as well as using Shift to sprint. Look around using your Mouse and press F1 to pause/unpause the game";
            }
            else if (gameObject.tag == "Double Jump")
            {
                hinttxt.text = "Press Space to jump, press it again in the air to double jump, you have two jump charges meaning if you fall from somewhere you can also jump twice midair, use this to your advantage";
            }
            else if (gameObject.tag == "Wall Climb")
            {
                hinttxt.text = "To climb a wall press space while touching it and moving towards it in any direction. as long as you are body height with any ledge you can climb up it";
            }
            else if (gameObject.tag == "Wall Run")
            {
                hinttxt.text = "To wall run simply run alongside a wall and press space whilst moving along the wall, you can also jump off a wall to gain greater distance";
            }
            else if(gameObject.tag == "Obstacle_tut")
            {
                hinttxt.text = "Falling into a pit or being touched by a Guard will reset you to your last checkpoint or your last picked up key, depending on what happened last";
            }
            else if(gameObject.tag == "Door_tut")
            {
                hinttxt.text = "Doors are opened automatically when you approach them";
            }
        }

    }
    // on trigger exit, remove the hint
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hinttxt.text = "";
        }
    }
}
