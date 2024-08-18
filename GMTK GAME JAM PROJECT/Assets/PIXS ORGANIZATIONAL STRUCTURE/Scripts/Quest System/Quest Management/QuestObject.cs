using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{
  
  public bool inTrigger = false;
  
   public List<int> availableQuestIDs = new List<int>();
   public List<int> recievableQuestIDs = new List<int>();

   public GameObject questMarker;
   public Image theImage;

   public Sprite questAvailableSprite;
   public Sprite questRecieveableSprite;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        SetQuestMarker();
    }

    void SetQuestMarker()
    {
        if(QuestManager.questManager.CheckCompletedQuest(this))
        {
           questMarker.SetActive(true);
           theImage.sprite = questRecieveableSprite;
           theImage.color = Color.yellow;
        }

        else if(QuestManager.questManager.CheckAvailableQuest(this))
        {
           questMarker.SetActive(true);
           theImage.sprite = questAvailableSprite;
           theImage.color = Color.yellow;
        }

        else if(QuestManager.questManager.CheckAcceptedQuest(this))
        {
           questMarker.SetActive(true);
           theImage.sprite = questAvailableSprite;
           theImage.color = Color.blue;
        }

        else
        {
            questMarker.SetActive(true);
            theImage.color = Color.grey;

        }


    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger && Input.GetKeyUp(KeyCode.Space))
        {
            //quest UI manager

            //Edited Out
            //QuestManager.questManager.QuestRequest(this);


            QuestUIManager.uiManager.CheckQuests(this);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
           inTrigger = true;
        }
        //you could use this with a timer to confirm mission completed
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
           inTrigger = false;
        }
        //you could use this with a timer to confirm mission completed
    }
}
