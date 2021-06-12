using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : StateMachineBehaviour
{
    public AudioClip[] clips;

    public enum PlayTime { OnEnter, OnUpdate, OnExit }
    public PlayTime whenToPlay;

    AudioSource source;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source = animator.gameObject.GetComponent<AudioSource>(); 
        if (source == null) source = animator.gameObject.GetComponentInParent<AudioSource>();
        if (source == null) source = animator.gameObject.GetComponentInChildren<AudioSource>();

        if (whenToPlay == PlayTime.OnEnter)
        {
            if (clips != null && clips.Length > 0)
            {
                source.PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
            }
            
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (whenToPlay == PlayTime.OnUpdate)
        {
            if (clips != null && clips.Length > 0)
            {
                source.PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (whenToPlay == PlayTime.OnExit)
        {
            if (clips != null && clips.Length > 0)
            {
                source.PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
            }
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
