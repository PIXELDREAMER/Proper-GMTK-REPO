using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DialogueA
{
    // This class represents a dialogue between a character and the player in the game.
    // It contains the name of the Non-Player Character (NPC) that is speaking, and
    // an array of strings representing the individual sentences of the dialogue.

    // The 'NameofNPC' field stores the name of the NPC that is speaking.
    // This name is used to identify the NPC and display it in the dialogue UI.
    public string NameofNPC; 
    
    // The 'sentences' field is an array of strings that holds the individual sentences
    // of the dialogue. The [TextArea(3, 10)] attribute is used to specify the size
    // of the text area in the Unity editor when editing this field. It sets the 
    // height to 3 and the width to 10.
    [TextArea (3, 10)] 
    public string[] sentences;

}
