using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBehaviour : StateMachineBehaviour
{
    private GameObject player;
    private float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = Player.Instance.gameObject;
        speed = animator.gameObject.GetComponent<Boss>().Speed * 2;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, 
                player.transform.position, speed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
