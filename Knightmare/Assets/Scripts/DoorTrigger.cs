using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorTrigger : MonoBehaviour
{
    // this is the same script from the door tutorial with the addition of sounds
    private Animator _animator = null;
    // Use this for initialization

    

    void Start()
    {
        // get the childobject of the door gameobject
        _animator = transform.GetChild(1).GetComponent<Animator>();
        if (_animator == null)
            Debug.Log("can't find animator on the child gameobject");
    }

   

    void OnTriggerEnter(Collider other)
    {
        // if the player collides with the door open the door
        if (other.gameObject.tag == "Player")
        {

            
            AudioManager.instance.Play("Door Open");
            _animator.SetBool("isopen", true);
           


        }
    }

    void OnTriggerExit(Collider other)
    {
        // if the player leaves the door trigger close the door
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.Play("Door Close");
            _animator.SetBool("isopen", false);
        }
    }
}
