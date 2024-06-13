using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimationController : MonoBehaviour
{
    public Animator animator;
    public GameDirector gameDirector;
    private bool isRunning;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        isRunning = false;
    }

    private void Update()
    {

        //forward
        if (Input.GetKey(KeyCode.W))
        {
            //forward left
            if (Input.GetKey(KeyCode.A))
            {
                //NOTE:
                //0.125 degeri aslýnda 0 ile 0.25 arasýndaki degerden gelir
                //blend tree uzerinden automated thrasholdu kapatarak daha rahat sayilar secilebilir
                SetTriggerRunning(0.125f);
            }

            //forward right
            else if (Input.GetKey(KeyCode.D))
            {
                //NOTE:
                //0.875 degeri aslýnda 1 ile 0.75 arasýndaki degerden gelir
                //blend tree uzerinden automated thrasholdu kapatarak daha rahat sayilar secilebilir
                SetTriggerRunning( 0.875f);
            }

            //only forward
            else
            {
                SetTriggerRunning(0);
            }
        }

        //backward
        else if (Input.GetKey(KeyCode.S))
        {
            //backward left
            if (Input.GetKey(KeyCode.A))
            {
              SetTriggerRunning(0.375f);
            }

            //backward right
            else if (Input.GetKey(KeyCode.D))
            {
                SetTriggerRunning(0.625f);
            }

            //only backward
            else
            {
                SetTriggerRunning(0.5f);
            }
        }
        
        else
        {
            //left
            if (Input.GetKey(KeyCode.A))
            {
                SetTriggerRunning(0.25f);
            }

            //right
            if (Input.GetKey(KeyCode.D))
            {
                SetTriggerRunning(0.75f);
            }

        }

        if(Input.GetKeyDown(KeyCode.Space) && gameDirector.playerHolder.isTouchingGround)
        {
            animator.SetTrigger("Jump");
        }
        if(!isRunning)
        {
            animator.ResetTrigger("Run");
            animator.SetTrigger("Idle");
        }
    }

    public void SetTriggerRunning(float direction)
    {
        //NOTE:
        //Trigger set etmeden once onceki trigger degerini reset etmek olasi set edilememe hatalarini onler
        animator.ResetTrigger("Idle");
        //NOTE:
        //trigger adi string halinde animatora verilir
        //bu parametre barindan ekledigimiz trigger parametresi aslinda
        animator.SetTrigger("Run");
        //NOTE:
        //Ikinci parametredeki blend treedeki trashold degerine denk gelen animasyonu tetikler
        animator.SetFloat("RunDirectionBlend", direction);
        isRunning = true;
    }
}
