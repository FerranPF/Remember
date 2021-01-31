using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private GameObject interactivePanel;
    private GameObject fadeInOut;
    private GameObject dialoguePanel;
    public Text interactiveText;

    private PlayerController playerController;

    public Animator levelDoors;

    public bool dialogue = false;

    private void Awake()
    {
        interactivePanel = GameObject.Find("InteractivePanel");
        fadeInOut = GameObject.Find("FadeInOut");
        dialoguePanel = GameObject.Find("DialoguePanel");
        levelDoors = GameObject.FindGameObjectWithTag("Door").GetComponent<Animator>();
        
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        dialoguePanel.SetActive(false);
        interactivePanel.SetActive(false);
    }

    public void NextLevel()
    {
        fadeInOut.SetActive(true);
        fadeInOut.GetComponent<Animator>().SetBool("fade", true);
    }

    public void OpenDoor()
    {
        levelDoors.SetBool("open", true);
    }

    public void OpenInteractivePanel(int interactiveType, string textShown)
    {
        switch (interactiveType)
        {
            case 0:
                interactivePanel.SetActive(true);
                interactiveText.text = textShown;
                break;
            case 1:
                interactivePanel.SetActive(true);
                interactiveText.text = textShown;
                levelDoors.SetBool("open", true);
                break;
            default:
                break;
        };
    }

    public void CloseInteractivePanel()
    {
        interactivePanel.SetActive(false);
    }

    public void OpenDialogue()
    {
        CloseInteractivePanel();
        playerController.LockMovement();

        dialogue = true;
        playerController.dialogue = dialogue;
        dialoguePanel.SetActive(true);
    }

    public void NextDialogue()
    {
        dialoguePanel.GetComponent<DialogueSystem>().NextDialogue();
    }

    public void CloseDialogue()
    {
        playerController.ResetMovement();

        dialogue = false;
        playerController.dialogue = dialogue;
        dialoguePanel.SetActive(false);
    }

    public void ChangeScene(string sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
