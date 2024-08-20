
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogueTriggerToAttach : MonoBehaviour
{
    public GameObject DialogueManagerObject;
    //public GameObject InteractButton;
    public Collider2D NpcdialogueTrigger;

    public int DialogueTriggerVisitCounter = 0; //This is used to keep track of how many times a dialogue trigger has been entered

    public DialogueTrigger dialogueTrigger;

    void OnTriggerEnter2D(Collider2D collider)
    {
        //To check region hasn't been visited before
        if (collider.CompareTag("Player"))
        {
            if(DialogueTriggerVisitCounter == 0)
            {
            DialogueManagerObject.SetActive(true);
            dialogueTrigger.TriggerDialogue();
            //InteractButton.SetActive(true);
            Debug.Log("Player has entered the NpcdialogueTrigger, tap button to speak with NPC");

            DialogueTriggerVisitCounter++;
            }

            else 
             return;

        }
        else
        {
            Debug.Log("NpcdialogueTrigger is not set right");
            Debug.Log("Region Has already Been Visited");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            //DialogueManagerObject.SetActive(false);
            
            Debug.Log("Player has exited the NpcdialogueTrigger");
        }
        else
        {
            Debug.Log("NpcdialogueTrigger is not set right");
        }
    }
}
