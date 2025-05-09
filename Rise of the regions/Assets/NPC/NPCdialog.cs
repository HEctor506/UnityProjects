using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCdialogue", menuName = "NPC Dialogue")]
public class NPCdialog : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool [] autoProgressLines;
    public bool [] endDialogueLines; //Mark where dialogue ends
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePich = 1f;

    public DialogueChoice[] choices;
}


[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex; //Dialogue line where choices appear
    public string[] choices; //player reponse options
    public int[] nextDialogueIndexes; //Where choice leads
}
