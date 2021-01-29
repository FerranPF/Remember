using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    Animator animator;
    bool open = false;
    bool isAnimating = false;

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
            }
        }
    }

    public void IsAnimating()
    {
        isAnimating = false;
    }

}
