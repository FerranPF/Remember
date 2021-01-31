using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    public string[] dialogues;
    public int dialoguePosition;
    public Text dialogueText;
    public bool dialogue = false;
    public bool finalScreen = false;

    public string sceneName;

    private void Start()
    {
        dialoguePosition = 0;
        dialogueText.text = dialogues[dialoguePosition];
        dialoguePosition++;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogue)
        {
            dialogueText.text = dialogues[dialoguePosition];
            dialoguePosition++;

            if (dialoguePosition == dialogues.Length)
            {
                dialogue = false;
            }
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<Animator>().SetBool("fade", true);
            dialogueText.text = "";
        }
    }

    public void ChangeScene()
    {
        if (!finalScreen)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Application.Quit();
        }
    }
}
