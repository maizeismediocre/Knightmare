using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter : MonoBehaviour
{
    // this script is based off the teleporter script from the sci-fi game assignment with some modifications such as the hint text and the key unlock system as well as disabling the player movement script so they can be teleported properly
    public GameObject destination;
    public GameObject particleprefab;
    public GameObject key; // Reference to the key GameObject
    private bool isUnlocked = false; // Track if the teleporter is unlocked
    GameObject player;
    public TMP_Text hinttxt;
    FirstPersonController playerController;
    CharacterController characterController;
    StarterAssetsInputs playerInputs;
    private Renderer teleporterBaseRenderer; // Reference to the teleporter base's renderer

    private void Start()
    {
        hinttxt.text = "";
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerController = player.GetComponent<FirstPersonController>();
            characterController = player.GetComponent<CharacterController>();
            playerInputs = player.GetComponent<StarterAssetsInputs>();
        }

        // Initialize the teleporter base's renderer by getting the Renderer component from the parent object
        if (transform.parent != null)
        {
            teleporterBaseRenderer = transform.parent.GetComponent<Renderer>();
        }

        SetTeleporterColor(Color.red); // Set initial color to red (locked)
    }
    

    private void OnTriggerEnter(Collider other)
    {
        //if the teleporter is locked , display a hint to the player
        if (!isUnlocked)
        {
            hinttxt.text = "Find the key to unlock the teleporter";
        }
        if (other.gameObject.tag == "Player" && isUnlocked)
        {
            //play teleporter sound
            FindAnyObjectByType<AudioManager>().Play("Teleporter");
            // Disable the movement script and reset inputs
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
                player.transform.position = destination.transform.position;
                characterController.enabled = true; // Re-enable CharacterController
            }
            else
            {
                player.transform.position = destination.transform.position;
            }

            // Re-enable the movement script
            if (playerController != null)
            {
                playerController.enabled = true;
            }

            Vector3 pos = destination.transform.position + 0.2f * Vector3.up; // position up a bit
            GameObject Particle = Instantiate(particleprefab, pos, Quaternion.identity);
            Destroy(Particle, 0.5f);
        }
    }
    // on exit of the trigger, remove the hint
    private void OnTriggerExit(Collider other)
    {
        hinttxt.text = "";
    }
    public void UnlockTeleporter()
    {
        isUnlocked = true;
        SetTeleporterColor(Color.green); // Change color to green (unlocked)
    }

    private void SetTeleporterColor(Color color)
    {
        if (teleporterBaseRenderer != null)
        {
            teleporterBaseRenderer.material.color = color;
        }
    }
}
