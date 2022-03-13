using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : Singleton<PlayerAnimationController>
{
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void RunAnimation(){
        animator.SetBool("Run",true);
    }

    public void GameOverAnimation(){
        animator.SetBool("Run", false);
        animator.SetBool("Sad", true);
    }

     public void FinishAnimation(){
        animator.SetBool("Run", false);
    }
}
