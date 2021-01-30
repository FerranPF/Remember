using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    public string[] dialogue;
    public Text textDialogue;
    public Text inputKeyText;

    private int dialoguePosition = 0;
    public int[] dialogueEnd;
    private bool firstScreen = true;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
    }

    private void Start()
    {
        NextDialogue(0);
    }

    public void NextDialogue(int level)
    {
        if (dialoguePosition == dialogueEnd[level])
        {
            CloseDialogue();
        }
        else
        {
            textDialogue.text = dialogue[dialoguePosition];
            inputKeyText.text = "Press E to continue";

            dialoguePosition++;
        }
    }

    public void CloseDialogue()
    {
        if (firstScreen)
        {
            gameManager.CloseFirstScreen();
            firstScreen = false;
        }
        else
        {
            gameManager.CloseDialogue();
        }
    }

}
