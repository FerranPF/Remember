using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    Animator animator;
    bool open = false;
    bool isAnimating = false;

    public bool giveObject = false;
    public GameObject interactiveGO;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation()
    {
        if (!isAnimating)
        {
            if (open)
            {
                animator.SetBool("open", false);
                open = false;
                isAnimating = true;
            }
            else
            {
                animator.SetBool("open", true);
                open = true;
                isAnimating = true;
                if (giveObject)
                {
                    bool key = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().key;
                    if (!key)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetKey();
                        Destroy(interactiveGO);
                        giveObject = false;
                    }
                }
            }
        }
    }

    public void IsAnimating()
    {
        isAnimating = false;
    }

}
