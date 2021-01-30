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
    bool door = false;


    public bool key = false;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        animator = gameObject.GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.z = Input.GetAxis("Vertical");

            this.transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

            if (moveDirection != Vector3.zero)
            {
                animator.SetBool("walk", true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection.normalized), 0.2f);
            }
            else
            {
                animator.SetBool("walk", false);
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
                LockMovement();
            }
        }

        if (door)
        {
            if (key)
            {
                gameManager.OpenInteractivePanel(2);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    UseKey();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameManager.OpenDialogue();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.NextDialogue();
        }
    }

    public void GetKey()
    {
        key = true;
        gameManager.OpenInteractivePanel(1);
    }

    public void UseKey()
    {
        gameManager.NextLevel();
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
            gameManager.OpenInteractivePanel(0);
            specialObject = true;
            collInteractive = other;
        }

        if (other.tag == "Door")
        {
            door = true;
        }

    }

    public void LockMovement()
    {
        canMove = false;
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
            gameManager.CloseInteractivePanel();
            specialObject = false;
            collInteractive = null;
        }

        if (other.tag == "Door")
        {
            door = false;
        }
    }

    public void ResetPlayer()
    {
        ResetMovement();
        key = false;
        door = false;
        specialObject = false;
        canInteract = false;
    }
}
