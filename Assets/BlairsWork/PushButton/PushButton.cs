﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public Animator anim;

    public bool isPressed = false;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if(!isPressed && !isActive)
        {
            Debug.Log("Pressed");
            isPressed = true;
            isActive = true;
            StartCoroutine(AnimationButtonPress(0.3f));
        }

        //if (!isActive)
            //isActive = true;

        //Debug.Log("pressed");
        //if(!isPressed && !isPlaying(anim, "PushButtonPressed"))
        //{
        //    Debug.Log("Started");
        //    anim.ResetTrigger("ButtonPressed");
        //    isPressed = true;
        //    anim.SetTrigger("ButtonPressed");
        //}
    }

    IEnumerator AnimationButtonPress(float animLength)
    {
        Debug.Log("New stream started");
        anim.SetTrigger("ButtonPressed");
        yield return new WaitForSeconds(animLength);
        anim.ResetTrigger("ButtonPressed");
        isPressed = false;
    }

    //public bool isPlaying(Animator anim, string stateName)
    //{
    //    if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        isPressed = false;
    //        return false;
    //    }
    //}
}


////This gives you trigger like behavior from a bool
//private IEnumerator AnimatorSetFire(float animationLength)
//{
//    animator.SetBool("Fire", true);

//    //You can wait for seconds, frames, other coroutines, whatever u need 
//    here
//   yield return new WaitForSeconds(animationLength);

//    animator.SetBool("Fire", false);
//}
