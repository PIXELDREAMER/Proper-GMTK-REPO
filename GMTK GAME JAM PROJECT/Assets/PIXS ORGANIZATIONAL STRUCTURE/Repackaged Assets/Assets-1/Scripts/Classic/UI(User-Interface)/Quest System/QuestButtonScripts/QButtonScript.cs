
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QButtonScript : MonoBehaviour
{
    public int questID;
    public TextMeshProUGUI questTitle;


   //Button References
   
   //[SerializeField]
    public GameObject acceptButton;

    //[SerializeField]
    public GameObject giveUpButton;

    //[SerializeField]
    public GameObject completeButton;


    //Connections to QButtonScript for each relevant button
    

  
    //Show All Infos
    public void ShowAllInfos()
    {
        QuestUIManager.uiManager.ShowSelectedQuest(questID);
       

       //Accept Button Functionality
        if(QuestManager.questManager.RequestAvailableQuest(questID))
        {
            QuestUIManager.uiManager.acceptButton.SetActive(true);
            QuestUIManager.uiManager.acceptButtonScript.questID = questID;
        }

        else
        {
            QuestUIManager.uiManager.acceptButton.SetActive(false);
        }

        //Give Up Button Functionality
         if(QuestManager.questManager.RequestAcceptedQuest(questID))
        {
            QuestUIManager.uiManager.giveUpButton.SetActive(true);
            QuestUIManager.uiManager.giveUpButtonScript.questID = questID;
        }

        else
        {
            QuestUIManager.uiManager.giveUpButton.SetActive(false);
        }

        //Complete Button Functionality
         if(QuestManager.questManager.RequestAcceptedQuest(questID))
        {
            QuestUIManager.uiManager.completeButton.SetActive(true);
            QuestUIManager.uiManager.completeButtonScript.questID = questID;
        }

        else
        {
            QuestUIManager.uiManager.completeButton.SetActive(false);
        }



    }

    public void AcceptQuest()
    {
      QuestManager.questManager.AcceptQuest(questID);
      QuestUIManager.uiManager.HideQuestPanel();

      //update all questgivers
      QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

      foreach(QuestObject obj in currentQuestGuys)
      {
        //Set QuestMarker
        obj.SetQuestMarker();
      }
    }
    
    //Attached to the OnClick
    public void GiveUpQuest()
    {
      QuestManager.questManager.GiveUpQuest(questID);
      QuestUIManager.uiManager.HideQuestPanel();

      //update all questgivers
      QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

      foreach(QuestObject obj in currentQuestGuys)
      {
        //Set QuestMarker
        obj.SetQuestMarker();
      }
    }



     public void CompleteQuest()
    {
      QuestManager.questManager.CompleteQuest(questID);
      QuestUIManager.uiManager.HideQuestPanel();

      //update all questgivers
      QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

      foreach(QuestObject obj in currentQuestGuys)
      {
        //Set QuestMarker
        obj.SetQuestMarker();
      }
    }


    public void ClosePanel() 
    {
        QuestUIManager.uiManager.HideQuestPanel();
        QuestUIManager.uiManager.acceptButton.SetActive(false);
        QuestUIManager.uiManager.giveUpButton.SetActive(false);
        QuestUIManager.uiManager.completeButton.SetActive(false);
    }


}
