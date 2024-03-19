using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool IsDialogue = true;
    public void TriggerDialogue()
    {
        if (DialogueManager.instance != null) // Kiểm tra null trước khi sử dụng
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }
        else
        {
            Debug.LogError("DialogueManager instance is null in TriggerDialogue!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Boss")
        {
            if (IsDialogue)
            {
                TriggerDialogue();
                IsDialogue = false;
            }           
        }
    }
}