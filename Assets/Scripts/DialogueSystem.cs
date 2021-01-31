using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    public string[] dialogue;
    public Text textDialogue;
    public Text inputKeyText;

    public int dialoguePosition = 0;
    public bool firstScreen = false;

    GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
    }

    public void NextDialogue()
    {
        Debug.Log("Next dialogue");
        if (dialoguePosition == dialogue.Length)
        {
            gameManager.OpenDoor();
            gameManager.CloseDialogue();
        }
        else
        {
            textDialogue.text = dialogue[dialoguePosition];
            inputKeyText.text = "Presiona E para seguir";

            dialoguePosition++;
        }
    }
}
