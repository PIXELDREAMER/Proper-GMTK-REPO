using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;

     
    public List<Quest> questList = new List<Quest>();           // Master Quest List
    public List<Quest> currentQuestList = new List<Quest>();    // Current Quest List

    // Private vars for our Quest Objects
    void Awake()
    {
        if (questManager == null)
        {
            questManager = this;
        }
        else if (questManager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void QuestRequest(QuestObject NPCQuestObject)
    {
        if(NPCQuestObject.availableQuestIDs.Count > 0)
        {
            for(int i = 0; i < questList.Count; i++)
            {
                for(int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
                {
                    if(questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                    {
                        Debug.Log("Quest ID: " + NPCQuestObject.availableQuestIDs[j] + " " + questList[i].progress);

                        //Testing
                       // AcceptQuest(NPCQuestObject.availableQuestIDs[j]);

                       QuestUIManager.uiManager.questAvailable = true;
                       QuestUIManager.uiManager.avaiableQuests.Add(questList[i]);
                    }
                }
            }
        }

        //ACTIVE QUESTS
        for(int i = 0; i< currentQuestList.Count; i++)
        {
          for(int j = 0; j < NPCQuestObject.recievableQuestIDs.Count; j++)
          {
             if(currentQuestList[i].id == NPCQuestObject.recievableQuestIDs[j] && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED || currentQuestList[i].progress == Quest.QuestProgress.COMPLETED)
             {
               
                Debug.Log("Quest ID:" + NPCQuestObject.recievableQuestIDs[j] + " is " + currentQuestList[i].progress);

               // CompleteQuest(NPCQuestObject.recievableQuestIDs[j]);
                //Quest UI Manager
                QuestUIManager.uiManager.questRunning = true;
                QuestUIManager.uiManager.activeQuests.Add(questList[i]);
                
             }
          }
        }

    }



//ACCEPT QUEST
public void AcceptQuest(int questID)
{
    for(int i = 0; i < questList.Count; i++ )
    {
        if((questList[i].id == questID) && questList[i].progress == Quest.QuestProgress.AVAILABLE)
        {
            currentQuestList.Add(questList[i]);
            questList[i].progress = Quest.QuestProgress.ACCEPTED;
        }
    }
}

//GIVE UP QUEST
public void GiveUpQuest(int questID)
{
    for(int i = 0; i < currentQuestList.Count; i++)
    {
     if(currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
     {
        currentQuestList[i].progress = Quest.QuestProgress.AVAILABLE;
        currentQuestList[i].questObjectiveCount = 0;
        currentQuestList.Remove(currentQuestList[i]);
     }
    }
}

//COMPLETE QUEST
public void CompleteQuest(int questID)
{
    for(int i = 0; i < currentQuestList.Count; i++)
    {
        if(currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.COMPLETED)
        {
            currentQuestList[i].progress = Quest.QuestProgress.COMPLETED;
            currentQuestList.Remove(currentQuestList[i]);

            //Rewards

        }
    }

    //Check for chainquests
    CheckChainQuest(questID);
}

//Check Chain Quest
void CheckChainQuest(int questID)
{
    int tempID = 0;

    for(int i = 0; i < questList.Count; i++)
    {
        if(questList[i].id == questID && questList[i].nextQuest > 0)
        {
            tempID = questList[i].nextQuest;
        }

        if(tempID > 0)
        {
            for(int j = 0; j < questList.Count; j++)
            {
                if(questList[i].id == tempID && questList[i].progress == Quest.QuestProgress.NOT_AVAILABLE)
                {
                   questList[i].progress = Quest.QuestProgress.AVAILABLE;
                }
            }
        }

    }
}


//ADD ITEMS
public void AddQuestItem(string questObjective, int itemAmount )
{
    for (int i = 0; i < currentQuestList.Count; i++)
    {
        if((currentQuestList[i].questObjective == questObjective) && (currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED))
        {
            currentQuestList[i].questObjectiveCount += itemAmount;
        }

        if((currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement) && (currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED) )
        {
            currentQuestList[i].progress = Quest.QuestProgress.DONE;
            currentQuestList.Remove(currentQuestList[i]);
        }
    }
}

//REMOVE ITEMS



    // BOOLEAN FUNCTIONS

    //Bool function for requesting available quest state
   
   //Bools 1
    public bool RequestAvailableQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }

        return false;
    }

    //Bool function for requesting Accepted Quests
     public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                return true;
            }
        }

        return false;
    }
    
    //Bool function for requesting completed quests
     public bool RequestCompletedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETED)
            {
                return true;
            }
        }

        return false;
    }
    
    //BOOLS 2
    public bool CheckAvailableQuest(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
           for(int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
           {
               if(questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
               {
                   return true;
               }
               
           }

           
        }
        return false;
    }

    public bool CheckAcceptedQuest(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
           for(int j = 0; j < NPCQuestObject.recievableQuestIDs.Count; j++)
           {
               if(questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.ACCEPTED)
               {
                   return true;
               }
               
           }

           
        }
        return false;
    }

    public bool CheckCompletedQuest(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
           for(int j = 0; j < NPCQuestObject.recievableQuestIDs.Count; j++)
           {
               if(questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.COMPLETED)
               {
                   return true;
               }
               
           }

           
        }
        return false;
    }



    // Start is called before the first frame update
    void Start()
    {
        // Initialize quests or other logic here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Handle quest updates or other logic here if needed
    }

    // This Coment is to test that GITHUB REPO WOrks
}
