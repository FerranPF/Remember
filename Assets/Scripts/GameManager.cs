using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private GameObject interactivePanel;

    private void Awake()
    {
        interactivePanel = GameObject.Find("InteractivePanel");
        interactivePanel.SetActive(false);
    }

    public void OpenInteractivePanel(int interactiveType)
    {
        switch (interactiveType)
        {
            case 0:
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

}
