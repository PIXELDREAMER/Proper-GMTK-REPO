using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogueTriggerToAttach : MonoBehaviour
{
    public GameObject DialogueManagerObject;
    public GameObject InteractButton;
    public Collider2D NpcdialogueTrigger;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            DialogueManagerObject.SetActive(true);
            InteractButton.SetActive(true);
            Debug.Log("Player has entered the NpcdialogueTrigger, tap button to speak with NPC");
        }
        else
        {
            Debug.Log("NpcdialogueTrigger is not set right");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            DialogueManagerObject.SetActive(false);
            
            Debug.Log("Player has exited the NpcdialogueTrigger");
        }
        else
        {
            Debug.Log("NpcdialogueTrigger is not set right");
        }
    }
}
