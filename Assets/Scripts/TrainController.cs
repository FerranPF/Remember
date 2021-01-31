using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    public Vector3 offset = new Vector3(0, 0, 0);

    bool placed = false;

    Vector3 initialPosition;
    Vector3 initialRotation;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if (placed)
        {
            this.transform.position = GameObject.FindGameObjectWithTag("ViewPosition").transform.position + offset;
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(v, 0, -h, Space.Self);

            if(GameObject.Find("DialoguePanel").GetComponent<DialogueSystem>().dialoguePosition 
                == GameObject.Find("DialoguePanel").GetComponent<DialogueSystem>().dialogue.Length)
            {
                ResetObject();
            }
        }

    }

    public void ResetObject()
    {
        placed = false;
        this.transform.position = initialPosition;
        this.transform.eulerAngles = initialRotation;
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>().OpenDoor();
    }

    public void ShowObject()
    {
        if (!placed)
        {
            placed = true;
        }
    }
}
