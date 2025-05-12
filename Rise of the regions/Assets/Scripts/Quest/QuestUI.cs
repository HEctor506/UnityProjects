using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public Transform questListContent;
    public GameObject questEntryPrefab;
    public GameObject objectivePrefab;
    
    public Quest testQuest;
    public int testQuestAmount;
    private List<QuestProgress> testQuests = new();

    void Start()
    {
        for(int i=0; i< testQuestAmount; i++)
        {
            testQuests.Add(new QuestProgress(testQuest));
        }
        UpdateQuestUI();
        
    }

    public void UpdateQuestUI()
    {
        //Destroy existing quest entries
        foreach(Transform child in questListContent)
        {
            Destroy(child.gameObject);
        }

        //Build quest entries
        foreach(var quest in testQuests)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContent);
            TMP_Text questNameText = entry.transform.Find("QuestName").GetComponent<TMP_Text>();
            Transform objetiveList = entry.transform.Find("ObjetiveList");

            questNameText.text = quest.quest.name;

            foreach(var objective in quest.objetives)
            {
                GameObject objTextGO = Instantiate(objectivePrefab, objetiveList);
                TMP_Text objText = objTextGO.GetComponent<TMP_Text>();
                objText.text = $"{objective.description} ({objective.currentAmount}/{objective.requiredAmount})";
            }
        }
    }
}
