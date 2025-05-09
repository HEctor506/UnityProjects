using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public string description;
    public List<QuestObjective> objetives;

    private void OnValidate()
    {
        if(string.IsNullOrEmpty(questID))
        {
            questID = questName + Guid.NewGuid().ToString();
        }
        
    }

    //To save to JSON, it needs to serializable
}

[System.Serializable]
    public class QuestObjective
    {
        public string objetiveID; //Match with item ID that you need to collect, enemy ID that you need to kill etc
        public string description;
        public ObjectiveType type;
        public int requiredAmount;
        public int currentAmount;

        public bool IsCompleted => currentAmount >= requiredAmount;

    }

    public enum ObjectiveType {ReachLocation, TalkNPC, Custom}


    [System.Serializable]
    public class QuestProgress
    {
        public Quest quest;
        public List<QuestObjective> objetives;

        public QuestProgress(Quest quest)
        {
            this.quest = quest;
            objetives = new List<QuestObjective>();

            //Deep copy avoid moifying original
            foreach(var obj in quest.objetives)
            {
                objetives.Add(new QuestObjective
                {
                    objetiveID = obj.objetiveID,
                    description = obj.description,
                    type = obj.type,
                    requiredAmount = obj.requiredAmount,
                    currentAmount = 0
                });
            }
        }

        public bool IsCompleted => objetives.TrueForAll(o => o.IsCompleted);
        public string QuestID => quest.questID;
    }
