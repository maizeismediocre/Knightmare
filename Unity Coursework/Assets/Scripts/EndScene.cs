using UnityEngine;

public class EndScene : MonoBehaviour
{
    //this script is original and was created by me

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //unlock mouse 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    private void Update()
    {
        // way of making it so it is called after the start method as to not interfere with the gameplay script 
        if (GamePlay.instance.gameover == false)
        {
            GamePlay.instance.goalreached();
        }
    }
}
