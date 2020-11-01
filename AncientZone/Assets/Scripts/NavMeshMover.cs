using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : MonoBehaviour
{
    public Vector3 currGoal;
    public NavMeshAgent navAgent;
    public float forwardMagnitude;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ScriptedGoalHandler goalHandler = this.gameObject.GetComponent<ScriptedGoalHandler>();

        if(goalHandler == null)
        {
            return;
        }

        BasicEnemyController enemyController = this.gameObject.GetComponent<BasicEnemyController>();

        if(enemyController.canGoToPrimaryTarget)
        {
            currGoal = enemyController.PrimaryTarget.transform.position;
        }
        else
        {
            currGoal = goalHandler.Goals[goalHandler.currentGoalIndex];
        }

        navAgent.destination = currGoal;

        Debug.DrawRay(this.transform.position, this.transform.forward * this.forwardMagnitude);
    }
}
