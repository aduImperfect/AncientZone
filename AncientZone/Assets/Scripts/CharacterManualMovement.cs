using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManualMovement : MonoBehaviour
{
    public float axisHVal;
    public float axisVVal;
    public bool isRunning;

    public float angleInDegreesToTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BasicEnemyController enemyController = this.gameObject.GetComponent<BasicEnemyController>();
        if (enemyController.canGoToPrimaryTarget)
        {
            InputHandler.HandleMovementInputTargeted(this.gameObject, this.axisVVal, this.isRunning, enemyController.PrimaryTarget);
            this.angleInDegreesToTarget = InputHandler.HandleRotationInputTargeted(this.gameObject, enemyController.PrimaryTarget);
        }
        else
        {
            ScriptedGoalHandler goalHandler = this.gameObject.GetComponent<ScriptedGoalHandler>();

            InputHandler.HandleMovementInputTargeted(this.gameObject, this.axisVVal, this.isRunning, goalHandler.Goals[goalHandler.currentGoalIndex]);
            this.angleInDegreesToTarget = InputHandler.HandleRotationInputTargeted(this.gameObject, goalHandler.Goals[goalHandler.currentGoalIndex]);
        }
    }
}
