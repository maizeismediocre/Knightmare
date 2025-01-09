// This is attached to each key, and is used to detecting if the player is overalapping the key

using UnityEngine;
using System.Collections;

public class PickupTrigger : MonoBehaviour
{
    // this script it based of the pickup trigger script from the sci-fi game assignment with some modifications such as the teleporter unlock
    public GameObject Particle;
    public Teleporter teleporter;

    private void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            // if the player collides with the key, unlock the teleporter and play the key sound
            teleporter.UnlockTeleporter();
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject smoke = Instantiate(Particle, pos, Quaternion.identity);
            Destroy(smoke, 1.0f);
            GamePlay.Instance.pickup();
            Destroy(gameObject);
            //set the players spawn to the current position
            GamePlay.Instance.setplayerspawn();
        }
    }
    
}
