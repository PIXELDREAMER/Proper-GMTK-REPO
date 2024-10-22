using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QLogButtonScript : MonoBehaviour
{
    public int questID;
    public TextMeshProUGUI questTitle;


   //Button References
   
   //[SerializeField]
    private GameObject acceptLogButton;

    //[SerializeField]
    private GameObject giveUpLogButton;

    //[SerializeField]
    private GameObject completeLogButton;


    //Connections to QButtonScript for each relevant button
    private QButtonScript acceptButtonScript;
    private QButtonScript giveUpButtonScript;
    private QButtonScript completeButtonScript;

    void Awake()
    {
        //Finding Accept Button
        acceptLogButton = GameObject.Find("QuestCanvas").transform.Find("Quest Log Panel").transform.Find("QUEST DESCRIPTION PANEL").transform.Find("Description Button Holder").transform.Find("Accept Button").gameObject;
        acceptButtonScript = acceptLogButton.GetComponent<QButtonScript>();

        //Finding Give Up Button
        giveUpLogButton = GameObject.Find("QuestCanvas").transform.Find("Quest Log Panel").transform.Find("QUEST DESCRIPTION PANEL").transform.Find("Description Button Holder").transform.Find("Give Up Button").gameObject;
        giveUpButtonScript = giveUpLogButton.GetComponent<QButtonScript>();
        
        //Finding Complete Button
        completeLogButton = GameObject.Find("QuestCanvas").transform.Find("Quest Log Panel").transform.Find("QUEST DESCRIPTION PANEL").transform.Find("Description Button Holder").transform.Find("Complete Button").gameObject;
        completeButtonScript = completeLogButton.GetComponent<QButtonScript>();
        
        //Setting Buttons To False
        acceptLogButton.SetActive(false);
        giveUpLogButton.SetActive(false);
        completeLogButton.SetActive(false);
    }

  
    //Show All Infos
    public void ShowAllInfos()
    {
        QuestManager.questManager.ShowQuestLog(questID);
       

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
        QuestUIManager.uiManager.HideQuestLogPanel();
    }

}
