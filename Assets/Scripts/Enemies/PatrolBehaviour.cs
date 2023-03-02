using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    private float speed;

    private GameObject[] patrolPoints;
    private int randomPoint;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("patrolPoints");
        randomPoint = Random.Range(0, patrolPoints.Length);
        speed = animator.gameObject.GetComponent<Boss>().Speed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = patrolPoints[randomPoint].transform.position;
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, 
            target, speed * Time.deltaTime);

        if (Vector2.Distance(animator.transform.position, target) < .1f)
        {
            randomPoint = Random.Range(0, patrolPoints.Length);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
