[System.Serializable]
public class Quest 
{
    //This enum holds the various states a quest can be in
    public enum QuestProgress
    {
      NOT_AVAILABLE,
      AVAILABLE,
      ACCEPTED,
      COMPLETED,
      DONE,
      FAILED,
    }

    //The following will be basic info about a quest:

    public string title;                     //Title of the quest
    public int id;                           //Id number for the current quest
    public QuestProgress progress;           //State of the current quest
    public string description;               //String from our quest giver
    public string hint;                      //String from our quest giver/reciever
    public string Congratulations;           //Congratulatory text
    public string Summary;                   //Text that summarizes the text
    public int nextQuest;                    //The Id number of the next quest
   


    public string questObjective;            //name of quest objective
    public int questObjectiveCount;          //current number of questObjective count
    public int questObjectiveRequirement;    // required amount of quest objective objects

    public int expReward;
    public int goldReward;
    public string itemReward;
    
}
