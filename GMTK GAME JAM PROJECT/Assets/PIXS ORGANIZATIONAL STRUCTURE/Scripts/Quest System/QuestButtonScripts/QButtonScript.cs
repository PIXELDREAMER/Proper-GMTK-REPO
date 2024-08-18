
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QButtonScript : MonoBehaviour
{
    public int questID;
    public TextMeshProUGUI questTitle;


   //Button References
    private GameObject acceptButton;
    private GameObject giveUpButton;
    private GameObject completeButton;


    //connections to QButtonScript
    private QButtonScript acceptButtonScript;
    private QButtonScript giveUpButtonScript;
    private QButtonScript completeButtonScript;

    void Start()
    {
        acceptButton = GameObject.FindGameObjectWithTag("AcceptButton");
        giveUpButton = GameObject.FindGameObjectWithTag("GiveUpButton");
        completeButton = GameObject.FindGameObjectWithTag("AcceptButton");
    }

  
    //Show All Infos
    public void ShowAllInfos()
    {
        QuestUIManager.uiManager.ShowSelectedQuest(questID);
    }


}
