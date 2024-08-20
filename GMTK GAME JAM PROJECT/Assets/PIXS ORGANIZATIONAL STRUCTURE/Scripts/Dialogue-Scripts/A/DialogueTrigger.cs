using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DialogueTrigger : MonoBehaviour
    {
        // This public field declares a variable named 'dialogue' which is of type 'Dialogue'. 
        // This variable will hold a reference to a Dialogue object.
        public Dialogue dialogue;

        // This is a public function named 'TriggerDialogue'. 
        // It doesn't take any parameters and doesn't return anything.
        public void TriggerDialogue()
        {
            // This line of code finds the first instance of a DialogueManager in the scene. 
            // It then calls the 'StartDialogue' function on that DialogueManager, 
            // passing in the 'dialogue' variable as an argument.
            // This effectively starts the dialogue for the player.
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue); 
        }

    }
}
