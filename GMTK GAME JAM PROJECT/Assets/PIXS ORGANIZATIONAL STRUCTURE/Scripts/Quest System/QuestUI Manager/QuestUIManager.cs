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
    private GameObject acceptButton;
    private GameObject giveUpButton;
    private GameObject completeButton;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(QuestKey))
        {
           questPanelActive = !questPanelActive;

           //show quest panel
           QuestPanel.SetActive(questPanelActive);

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


    //Show Panel
    public void ShowQuestPanel()
    {
        questPanelActive = true;
        QuestPanel.SetActive(questPanelActive);

        //Fill In Data
        FillQuestButtons();

    }

    //quest Log Panel



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
