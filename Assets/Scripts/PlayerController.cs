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

    bool canMove = true;
    bool specialObject = false;
    bool door = false;
    public bool train = false;
    bool keyCabinet = false;
    bool haveKeyCabinet = false;
    bool canOpenCabinet = false;
    bool console = false;
    bool haveGame = false;
    bool friendImage = false;
    bool charger = false;
    bool haveCharger = false;
    bool phone = false;
    bool carKey = false;
    bool havePhone = false;

    public bool dialogue = false;

    GameObject dialoguePanel;
    GameObject gameStick;
    GameObject chargerObj;

    public bool key = false;
    public string cabinetMessage;
    public string deskMessage;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        animator = gameObject.GetComponentInChildren<Animator>();

        gameStick = GameObject.Find("GameStick");
        if(gameStick != null)
        {
            gameStick.SetActive(false);
        }

        chargerObj = GameObject.Find("Charger");
        if (chargerObj != null)
        {
            chargerObj.SetActive(false);
        }

        dialoguePanel = GameObject.Find("DialoguePanel");

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
        if (specialObject)
        {
            SpecialObject specialObject = collInteractive.GetComponent<SpecialObject>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                specialObject.ShowObject();
                LockMovement();
            }
        }

        if (train)
        {
            TrainController trainController = collInteractive.GetComponent<TrainController>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                trainController.ShowObject();
                key = true;

                gameManager.OpenDialogue();
                train = false;
                GameObject.FindGameObjectWithTag("Train").GetComponent<BoxCollider>().enabled = false;
            }
        }

        if (friendImage)
        {
            FriendImage friendImageObj = collInteractive.GetComponent<FriendImage>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                friendImageObj.ShowObject();
                key = true;

                gameManager.OpenDialogue();
                friendImage = false;
                GameObject.FindGameObjectWithTag("FriendImage").GetComponent<BoxCollider>().enabled = false;
            }
        }

        if (door)
        {
            if (key)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    UseKey();
                }
            }
        }

        if (dialogue)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameManager.NextDialogue();
            }
        }

        if (keyCabinet)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                haveKeyCabinet = true;
                Destroy(GameObject.Find("Key"));
                gameManager.CloseInteractivePanel();
                keyCabinet = false;
            }
        }

        if (canOpenCabinet)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                collInteractive.GetComponent<Animator>().SetBool("open", true);
                haveGame = true;
                gameManager.OpenInteractivePanel(0, "Ya tengo el juego");
            }
        }

        if (console)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                key = true;
                gameStick.SetActive(true);
                gameManager.OpenDialogue();
                gameManager.NextDialogue();
                console = false;

                GameObject.FindGameObjectWithTag("Console").GetComponent<BoxCollider>().enabled = false;
            }
        }

        if (phone)
        {
            if (Input.GetKeyDown(KeyCode.E) && !havePhone)
            {
                key = true;
                gameManager.OpenDialogue();
                gameManager.NextDialogue();
                phone = false;
                chargerObj.SetActive(true);
                havePhone = true;
            }
        }

        if (charger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                charger = false;
                Destroy(collInteractive.gameObject);
                haveCharger = true;
                gameManager.CloseInteractivePanel();
            }
        }

        if (carKey)
        {
            CarKey carKeyObj = collInteractive.GetComponent<CarKey>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                carKeyObj.ShowObject();
                key = true;

                gameManager.OpenDialogue();
                gameManager.NextDialogue();
                carKey = false;
                GameObject.FindGameObjectWithTag("CarKey").GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void UseKey()
    {
        gameManager.NextLevel();
        LockMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SpecialObject")
        {
            gameManager.OpenInteractivePanel(0, "E para ver");
            specialObject = true;
            collInteractive = other;
        }

        if (other.tag == "Train")
        {
            gameManager.OpenInteractivePanel(0, "E para interactuar");
            train = true;
            collInteractive = other;
        }

        if(other.tag == "FriendImage")
        {
            gameManager.OpenInteractivePanel(0, "E para interactuar");
            friendImage = true;
            collInteractive = other;
        }

        if (other.tag == "Door")
        {
            if (key)
            {
                gameManager.OpenInteractivePanel(1, "E para abrir");
            }
            else
            {
                gameManager.OpenInteractivePanel(0, "Parece que esta cerrada");
            }
            door = true;
        }

        if (other.tag == "Key")
        {
            gameManager.OpenInteractivePanel(0, "E para coger");
            keyCabinet = true;
            collInteractive = other;
        }

        if(other.tag == "LockedCabinet")
        {
            if (haveKeyCabinet && !haveGame)
            {
                canOpenCabinet = true;
                gameManager.OpenInteractivePanel(0, "E para usar llave");
                collInteractive = other;
            }
            else if(!haveKeyCabinet)
            {
                canOpenCabinet = false;
                gameManager.OpenInteractivePanel(0, cabinetMessage);
            }
            else if(haveGame)
            {
                canOpenCabinet = false;
                gameManager.OpenInteractivePanel(0, "Ya tengo el juego");
            }
        }

        if(other.tag == "Console")
        {
            if (haveGame && !key)
            {
                console = true;
                gameManager.OpenInteractivePanel(0, "E para introducir cartucho");
            }
            else if(!haveGame)
            {
                console = false;
                gameManager.OpenInteractivePanel(0, "Parece que necesito un juego");
            }else if(haveGame && key)
            {
                console = false;
                gameManager.OpenInteractivePanel(0, "Ya he puesto el cartucho");
            }
        }

        if(other.tag == "Desk")
        {
            gameManager.OpenInteractivePanel(0, deskMessage);
        }

        if(other.tag == "iPhone")
        {
            if (haveCharger && !havePhone)
            {
                phone = true;
                gameManager.OpenInteractivePanel(0, "E para usar el cargador");
            }else if (!haveCharger)
            {
                phone = false;
                gameManager.OpenInteractivePanel(0, "Parece que no le queda batería");
            }else if (havePhone && haveCharger)
            {
                phone = false;
                gameManager.OpenInteractivePanel(0, "Prefiero no volverlo a ver");
            }
        }

        if(other.tag == "Charger")
        {
            if (!haveCharger)
            {
                charger = true;
                gameManager.OpenInteractivePanel(0, "E para coger el cargador");
                collInteractive = other;
            }
        }

        if (other.tag == "CarKey")
        {
            gameManager.OpenInteractivePanel(0, "E para interactuar");
            carKey = true;
            collInteractive = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SpecialObject")
        {
            gameManager.CloseInteractivePanel();
            specialObject = false;
            collInteractive = null;
        }

        if (other.tag == "Train")
        {
            gameManager.CloseInteractivePanel();
            train = false;
            collInteractive = null;
        }
        
        if (other.tag == "FriendImage")
        {
            gameManager.CloseInteractivePanel();
            friendImage = false;
            collInteractive = null;
        }

        if (other.tag == "Door")
        {
            door = false;
            collInteractive = null;
            gameManager.CloseInteractivePanel();
        }

        if (other.tag == "Key")
        {
            keyCabinet = false;
            collInteractive = null;
            gameManager.CloseInteractivePanel();
        }

        if (other.tag == "LockedCabinet")
        {
            gameManager.CloseInteractivePanel();
            canOpenCabinet = false;
        }

        if (other.tag == "Console")
        {
            gameManager.CloseInteractivePanel();
            console = false;
        }

        if(other.tag == "Desk")
        {
            gameManager.CloseInteractivePanel();
        }

        if (other.tag == "iPhone")
        {
            gameManager.CloseInteractivePanel();
            phone = false;
        }

        if (other.tag == "Charger")
        {
            charger = false;
            gameManager.CloseInteractivePanel();
        }

        if (other.tag == "CarKey")
        {
            gameManager.CloseInteractivePanel();
            carKey = false;
            collInteractive = null;
        }
    }

    public void LockMovement()
    {
        canMove = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void ResetMovement()
    {
        canMove = true;
        this.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void ResetPlayer()
    {
        ResetMovement();
        gameManager.CloseInteractivePanel();
    }
}
