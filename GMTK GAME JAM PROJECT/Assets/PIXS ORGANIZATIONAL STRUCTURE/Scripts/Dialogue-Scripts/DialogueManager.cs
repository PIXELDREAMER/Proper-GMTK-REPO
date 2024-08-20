using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
/*
This script manages dialogue in a game. It holds references to the UI elements that display the dialogue, such as the name of the NPC and the actual dialogue text.
It also holds a reference to the animator component, which controls the UI elements' visibility.
The sentences variable is a queue that holds all the sentences of a dialogue.
*/
public TextMeshProUGUI NameText; // UI element to display the name of the NPC
public TextMeshProUGUI DialogueText; // UI element to display the actual dialogue text
public Animator animator; // Reference to the animator component, controls the visibility of UI elements
public Queue <string> sentences; // Queue that holds all the sentences of a dialogue

public GameObject DialogueBoxPanel;

/*
This is the Start function, it is called before the first frame update.
In this function, we initialize the sentences queue.
*/
void Start()
{
    sentences = new Queue<string>(); // Initializing the sentences queue
    
}

/*
This function starts a new dialogue. It takes a Dialogue object as a parameter, which contains the name of the NPC and all the sentences of the dialogue.
*/
public void StartDialogue(Dialogue dialogue)
{
    animator.SetBool("isOpen" , true); // Showing the UI elements
    Debug.Log("Starting conversation with " + dialogue.NameofNPC); // Logging the name of the NPC

    NameText.text = dialogue.NameofNPC; // Setting the name of the NPC
    sentences.Clear(); // Clearing the sentences queue

    /*
    Adding each sentence of the dialogue to the sentences queue
    */
    foreach (string sentence in dialogue.sentences)
    {
        sentences.Enqueue(sentence);
 // Adding the sentence to the queue
    }

    DisplayNextSentence(); // Displaying the first sentence
}

/*
This function displays the next sentence in the sentences queue.
*/
public void DisplayNextSentence()
{
    if (sentences.Count == 0) // If there are no more sentences, end the dialogue
    {
        EndDialogue(); // Ending the dialogue
        return;
    }

    string sentence = sentences.Dequeue(); // Getting the next sentence
    StopAllCoroutines(); // Stop any previous coroutine displaying a sentence
    StartCoroutine(TypeSentence(sentence)); // Starting a new coroutine to display the sentence
    Debug.Log(sentence); // Logging the sentence
}

/*
This coroutine types out the sentence character by character.
*/
IEnumerator TypeSentence (string sentence)
{
    DialogueText.text = ""; // Clearing the dialogue text
    /*
    Typing out the sentence character by character
    */
    foreach(char letter in sentence.ToCharArray())
    {
        DialogueText.text += letter; // Adding the letter to the dialogue text
        yield return null; // Waiting for the next frame
    }
}


/*
This function ends the dialogue and hides the UI elements.
*/
public void EndDialogue()
{
    animator.SetBool("isOpen", false); // Hiding the UI elements
    Debug.Log("End of Conversation "); // Logging the end of the conversation
    DialogueBoxPanel.SetActive(false); // Hiding the dialogue panel
}

/*
This is the Update function, it is called once per frame.
In this function, we don't do anything.
*/

   

  
    // Update is called once per frame
    void Update()
    {
        
    }
}
