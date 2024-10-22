
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager uiManager;

    //Quest Key
    [Header("Quest Key")]
    public KeyCode QuestKey;

    // VARS
    public bool questAvailable = false;
    public bool questRunning = false;
    private bool questPanelActive = false;
    private bool  questLogPanelActive = false;

    //PANELS
    public GameObject QuestPanel;
    public GameObject QuestLogPanel;

    //Quest Object
    private QuestObject currentQuestObject;
   

    //Quest Lists
    
     public List<Quest> avaiableQuests = new List<Quest>();

   
     public List<Quest> activeQuests = new List<Quest>();

    
    //Buttons
    public GameObject qbutton;
    public GameObject qLogButton;

    private List<GameObject> qButtons = new List<GameObject>();
    public GameObject acceptButton;
    public GameObject giveUpButton;
    public GameObject completeButton;

    //Spacers
    public Transform qButtonSpacer1;
    public Transform qButtonSpacer2;
    public Transform qLogButtonSpacer;

    //QUEST INFOS
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questSummary;

    //QUEST LOG INFOS
    public TextMeshProUGUI questLogTitle;
    public TextMeshProUGUI questLogDescription;
    public TextMeshProUGUI questLogSummary;

   
   
    public QButtonScript acceptButtonScript;
    public QButtonScript giveUpButtonScript;
    public QButtonScript completeButtonScript;

    void Start()
    {
        //Finding Accept Button
        acceptButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("QUEST DESCRIPTION PANEL").transform.Find("Description Button Holder").transform.Find("Accept Button").gameObject;
        acceptButtonScript = acceptButton.GetComponent<QButtonScript>();

        //Finding Give Up Button
        giveUpButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("QUEST DESCRIPTION PANEL").transform.Find("Description Button Holder").transform.Find("Give Up Button").gameObject;
        giveUpButtonScript = giveUpButton.GetComponent<QButtonScript>();
        
        //Finding Complete Button
        completeButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("QUEST DESCRIPTION PANEL").transform.Find("Description Button Holder").transform.Find("Complete Button").gameObject;
        completeButtonScript = completeButton.GetComponent<QButtonScript>();
        
        //Setting Buttons To False
        acceptButton.SetActive(false);
        giveUpButton.SetActive(false);
        completeButton.SetActive(false);
    }
    


    void Awake()
    {
        if(uiManager == null)
        {
           uiManager = this; 
        }
        else if(uiManager != this)
        {
           Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        HideQuestPanel();
    }


    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(QuestKey))
        {
           questPanelActive = !questPanelActive;
           QuestPanel.SetActive(questPanelActive);
           
         // ShowQuestLogPanel();


        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            questLogPanelActive = !questLogPanelActive;

            ShowQuestLogPanel();
        }
    }

    //Called from quest object on NPC

    public void CheckQuests(QuestObject questObject)
    {
        QuestManager.questManager.QuestRequest(questObject);

        if((questRunning || questAvailable) && !questPanelActive)
        {
            //showquestLogpanel
        

           ShowQuestPanel();
        }

        else
        {
            Debug.Log("No Quests Available");
        }
    }
    

    //Hide Quest Panel
    public void HideQuestPanel() 
    {
        questPanelActive = false;
        questAvailable = false;
        questRunning = false;

        //Clearing Text
        questTitle.text = "";
        questDescription.text = "";
        questSummary.text = "";
        

        //Clearing All Quest Lists
        avaiableQuests.Clear();
        activeQuests.Clear();

        //Clearing Buttons Lists
        for(int i = 0; i <qButtons.Count ; i++) 
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();

        //turning off the panel's activity
        QuestPanel.SetActive(questPanelActive);

    }

    //Hide Quest Log Panel
    public void HideQuestLogPanel() 
    {
        questLogPanelActive = false;
       

        //Clearing Text
        questLogTitle.text = "";
        questLogDescription.text = "";
        questLogSummary.text = "";
        

        //Clearing All Quest Lists
        avaiableQuests.Clear();
        activeQuests.Clear();

        //Clearing Buttons Lists
        for(int i = 0; i <qButtons.Count ; i++) 
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();

        //turning off the panel's activity
        QuestLogPanel.SetActive(questPanelActive);
    }


    //Show Panel
    public void ShowQuestPanel()
    {
        questPanelActive = true;
        QuestPanel.SetActive(questPanelActive);

        //Fill In Data
        FillQuestButtons();

    }

//Show Quest Log
    public void ShowQuestLogPanel()
    {
        
        QuestLogPanel.SetActive(questLogPanelActive);

        if(questLogPanelActive && !questPanelActive)
        {
            foreach(Quest curQuest in QuestManager.questManager.currentQuestList)
            {
                GameObject questButton = Instantiate(qLogButton);
                QLogButtonScript qButton = questButton.GetComponent<QLogButtonScript>(); 

                qButton.questTitle.text = curQuest.title;

                questButton.transform.SetParent(qLogButtonSpacer, false);
                qButtons.Add(questButton);
            }
        }

        else if(!questLogPanelActive && !questPanelActive)
        {
            HideQuestLogPanel();
        }
    }

    public void ShowQuestLog(Quest activeQuest)
    {
        questLogTitle.text = activeQuest.title;
        if(activeQuest.progress == Quest.QuestProgress.ACCEPTED)
        {
            questLogDescription.text = activeQuest.hint;
            questLogSummary.text = activeQuest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequirement;
        }

        else if(activeQuest.progress == Quest.QuestProgress.COMPLETED)
        {
            questLogDescription.text = activeQuest.Congratulations;
            questLogSummary.text = activeQuest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequirement;
        }


    }

    //Int type
   



    //Fill Buttons For Quest Panel

    void FillQuestButtons()
    {
        //Available Quests
        foreach(Quest avaiableQuest in avaiableQuests) 
        {
            GameObject questButton = Instantiate(qbutton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = avaiableQuest.id;
            qBScript.questTitle.text = avaiableQuest.title;

            questButton.transform.SetParent(qButtonSpacer1, false);
            qButtons.Add(questButton);

        }
        
        //Active Quests
         foreach(Quest activeQuest in activeQuests) 
        {
            GameObject questButton = Instantiate(qbutton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = activeQuest.id;
            qBScript.questTitle.text = activeQuest.title;

            questButton.transform.SetParent(qButtonSpacer2, false);
            qButtons.Add(questButton);

        }
    }

    //Show Quest On Button Press In quest Panel
    public void ShowSelectedQuest(int questID)
    {
        //This will show necessary information on available quests panel
        for(int i = 0; i< avaiableQuests.Count; i++)
        {
            if(avaiableQuests[i].id == questID)
            {
                questTitle.text = avaiableQuests[i].title;

                if(avaiableQuests[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                   questDescription.text = avaiableQuests[i].description;
                   questSummary.text = avaiableQuests[i].questObjective + " : " +avaiableQuests[i].questObjectiveCount + " / " + avaiableQuests[i].questObjectiveRequirement;
                }


            }

            
        }

         for(int i = 0; i< activeQuests.Count; i++)
        {
            if(activeQuests[i].id == questID)
            {
                questTitle.text = activeQuests[i].title;

                if(avaiableQuests[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                   questDescription.text = activeQuests[i].hint;
                   questSummary.text = activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequirement;
                }

                else if(avaiableQuests[i].progress == Quest.QuestProgress.COMPLETED)
                {
                    questDescription.text = activeQuests[i].Congratulations;
                }


            }

            
        }
    }
}
