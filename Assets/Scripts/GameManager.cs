using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private GameObject interactivePanel;
    private GameObject keyPanel;
    private GameObject fadeInOut;
    private GameObject dialoguePanel;
    private GameObject firstScreen;

    private PlayerController playerController;

    private int level = 0;

    public Animator[] levelDoors;
    public GameObject[] levelPositions;

    public bool dialogue = false;


    float count = 0;
    private bool closeFirstScreen = false;

    private void Awake()
    {
        interactivePanel = GameObject.Find("InteractivePanel");
        keyPanel = GameObject.Find("KeyPanel");
        fadeInOut = GameObject.Find("FadeInOut");
        dialoguePanel = GameObject.Find("DialoguePanel");
        firstScreen = GameObject.Find("FirstScreen");
        
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        interactivePanel.SetActive(false);
        keyPanel.SetActive(false);
        firstScreen.SetActive(true);
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    IEnumerator NextLevelCoroutine()
    {

        fadeInOut.SetActive(true);
        fadeInOut.GetComponent<Animator>().SetBool("fade", true);
        playerController.LockMovement();

        yield return new WaitForSecondsRealtime(1.1f);

        playerController.transform.position = levelPositions[level].transform.position;

        yield return new WaitForSecondsRealtime(0.5f);

        CloseInteractivePanel();
        CloseKeyPanel();
        fadeInOut.GetComponent<Animator>().SetBool("fade", false);

        yield return new WaitForSecondsRealtime(0.9f);

        playerController.ResetPlayer();
        level++;
    }

    public void OpenDoor()
    {
        levelDoors[level].SetBool("open", true);
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        fadeInOut.SetActive(true);
        fadeInOut.GetComponent<Animator>().SetBool("fade", false);
        playerController.LockMovement();

        yield return new WaitForSeconds(1.1f);

        playerController.ResetMovement();
    }


    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        fadeInOut.SetActive(true);
        fadeInOut.GetComponent<Animator>().SetBool("fade", true);
        playerController.LockMovement();

        yield return new WaitForSeconds(1.1f);

        playerController.ResetMovement();
    }



    public void OpenInteractivePanel(int interactiveType)
    {
        switch (interactiveType)
        {
            case 0:
                interactivePanel.SetActive(true);
                break;
            case 1:
                if(level > 0)
                {
                    levelDoors[level].SetBool("open", true);
                }
                keyPanel.SetActive(true);
                break;
            case 2:
                interactivePanel.SetActive(true);
                break;
            default:
                break;
        };
    }

    public void CloseInteractivePanel()
    {
        interactivePanel.SetActive(false);
    }

    public void CloseKeyPanel()
    {
        keyPanel.SetActive(false);
    }

    public void OpenDialogue()
    {
        CloseInteractivePanel();
        playerController.LockMovement();

        dialogue = true;
        dialoguePanel.SetActive(true);
    }

    public void NextDialogue()
    {
        dialoguePanel.GetComponent<DialogueSystem>().NextDialogue(level);
    }

    public void CloseDialogue()
    {
        playerController.ResetMovement();

        dialogue = false;
        dialoguePanel.SetActive(false);
    }

    public void CloseFirstScreen()
    {
        closeFirstScreen = true;
        count = 0;
    }

    private void Update()
    {
        if (closeFirstScreen)
        {
            fadeInOut.SetActive(true);
            count += Time.deltaTime;

            if(count < 1.0f)
            {
                fadeInOut.GetComponent<Animator>().SetBool("fade", true);
                playerController.LockMovement();

            }else if(count < 2.2f)
            {
                firstScreen.SetActive(false);
                CloseDialogue();

            }else if(count < 2.6f)
            {
                playerController.transform.position = levelPositions[level].transform.position;
                fadeInOut.GetComponent<Animator>().SetBool("fade", false);

            }else if(count < 3.7f){

                playerController.ResetPlayer();
                level = 1;
            }
            else { closeFirstScreen = false; }
        }
    }

}
