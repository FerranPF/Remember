using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Vector3 moveDirection;

    Animator animator;
    GameManager gameManager;
    Collider collInteractive;

    bool canInteract = false;
    bool canMove = true;
    bool specialObject = false;

    bool key = false;

    private void Awake()
    {
        //animator.GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
    }


    void FixedUpdate()
    {
        if (canMove)
        {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.z = Input.GetAxis("Vertical");

            this.transform.Translate(moveDirection * speed, Space.World);

            if (moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection.normalized), 0.2f);
            }
        }
    }


    private void Update()
    {
        if (canInteract && canMove)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                InteractiveObject interactiveObject = collInteractive.GetComponent<InteractiveObject>();
                interactiveObject.TriggerAnimation();
            }
        }

        if (specialObject)
        {
            SpecialObject specialObject = collInteractive.GetComponent<SpecialObject>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                specialObject.ShowObject();
                canMove = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactive")
        {
            gameManager.OpenInteractivePanel(0);
            canInteract = true;
            collInteractive = other;
        }

        if(other.tag == "SpecialObject")
        {
            specialObject = true;
            collInteractive = other;
        }

    }

    public void ResetMovement()
    {
        canMove = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactive")
        {
            gameManager.CloseInteractivePanel();
            canInteract = false;
            collInteractive = null;
        }

        if (other.tag == "SpecialObject")
        {
            specialObject = false;
            collInteractive = null;
        }
    }
}
