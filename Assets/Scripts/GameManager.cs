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

    private PlayerController playerController;

    private int level = 0;

    public Animator[] levelDoors;
    public GameObject[] levelPositions;

    private void Awake()
    {
        interactivePanel = GameObject.Find("InteractivePanel");
        keyPanel = GameObject.Find("KeyPanel");
        fadeInOut = GameObject.Find("FadeInOut");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        interactivePanel.SetActive(false);
        keyPanel.SetActive(false);
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

        yield return new WaitForSeconds(1.5f);

        playerController.transform.position = levelPositions[level].transform.position;
        fadeInOut.SetActive(true);
        fadeInOut.GetComponent<Animator>().SetBool("fade", false);

        yield return new WaitForSeconds(0.5f);

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
                levelDoors[level].SetBool("open", true);
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

}
