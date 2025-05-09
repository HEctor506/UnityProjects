using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCdialog dialogueData;
    private DialogueController dialogueUI;

    private int dialogueIndex;
    private bool  isTyping, isDialogueActive;

    void Start()
    {
        dialogueUI = DialogueController.Instance;
    }

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        //If no dialogue data or the game is paused and no dialogue is active
        if(dialogueData == null || (Time.timeScale ==0 && !isDialogueActive)) return; 

        if(isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }

    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        dialogueUI.SetNPCinfo(dialogueData.npcName, dialogueData.npcPortrait);
        dialogueUI.ShowDialogue(true);
        Time.timeScale = 0; //to pause the game and player doesnt walk away

        //TypeLine
        DisplayCurrentLine();
    }

    void NextLine()
    {
        if(isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }

        //Clear choices
        dialogueUI.ClearChoices();

        //Check endDialogueLines
        if(dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }
        //Check if choice and display
        foreach(DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if(dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }


        if(++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            //If another line, type next line
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            SoundManager.instance.PlayVoice(dialogueData.voiceSound, dialogueData.voicePich);
            yield return new WaitForSecondsRealtime(dialogueData.typingSpeed);
        }

        isTyping = false;

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSecondsRealtime(dialogueData.autoProgressDelay);
            //Display next line
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for(int i=0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexes[i];
            dialogueUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
        }
    }

    void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogue(false);
        Time.timeScale = 1;
    }
}

