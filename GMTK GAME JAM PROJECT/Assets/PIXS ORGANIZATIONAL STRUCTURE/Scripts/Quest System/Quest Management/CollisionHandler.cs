using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneToLoad; //Scene to load
    public string spawnpointname;
    //public GameObject spawnpoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        QuestManager.questManager.AddQuestItem("Leave Town 1", 1);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
