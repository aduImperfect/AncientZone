using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public GameObject PrimaryTarget;
    
    public float timeInPrimaryMovementState;
    public float timeLimitForPrimaryMovementState;

    public float timeInSecondaryMovementState;
    public float timeLimitForSecondaryMovementState;

    public float maxMagnitudeToPlayer;

    public List<int> PrimaryMovementStatePercentages;
    public List<int> SecondaryMovementStatePercentages;

    public EnemyStateData.EnemyPrimaryMovementState ePrimaryMovementState;
    public EnemyStateData.EnemySecondaryMovementState eSecondaryMovementState;

    public int randPrimaryMovementPercentVal;
    public int randSecondaryMovementPercentVal;

    public bool canGoToPrimaryTarget;
    public float forwardMagnitude;

    public int primaryMinMovementPercent;
    public int primaryMaxMovementPercent;

    public int secondaryMinMovementPercent;
    public int secondaryMaxMovementPercent;

    public float primaryMinTimeLimitMovementState;
    public float primaryMaxTimeLimitMovementState;

    public float secondaryMinTimeLimitMovementState;
    public float secondaryMaxTimeLimitMovementState;

    public float maxMagnitudeToGoal;

    // Start is called before the first frame update
    void Start()
    {
        this.canGoToPrimaryTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.randPrimaryMovementPercentVal = Random.Range(primaryMinMovementPercent, primaryMaxMovementPercent);
        this.timeLimitForPrimaryMovementState = Random.Range(primaryMinTimeLimitMovementState, primaryMaxTimeLimitMovementState);
        this.timeInPrimaryMovementState += Time.deltaTime;

        this.randSecondaryMovementPercentVal = Random.Range(secondaryMinMovementPercent, secondaryMaxMovementPercent);
        this.timeLimitForSecondaryMovementState = Random.Range(secondaryMinTimeLimitMovementState, secondaryMaxTimeLimitMovementState);
        this.timeInSecondaryMovementState += Time.deltaTime;

        if (this.PrimaryTarget != null)
        {
            Vector3 distanceBwPlayer = this.transform.position - this.PrimaryTarget.transform.position;

            ScriptedGoalHandler goalHandler = this.gameObject.GetComponent<ScriptedGoalHandler>();
            if((Mathf.Abs(distanceBwPlayer.magnitude) < this.maxMagnitudeToPlayer) && (Mathf.Abs((goalHandler.Goals[goalHandler.currentGoalIndex] - this.transform.position).magnitude) < this.maxMagnitudeToGoal))
            {
                this.canGoToPrimaryTarget = true;
            }
            else
            {
                this.canGoToPrimaryTarget = false;
            }
        }
        else
        {
            this.canGoToPrimaryTarget = false;
        }

        this.PrimaryMovementState();
        this.SecondaryMovementState();

        if (this.canGoToPrimaryTarget)
        {
            Debug.DrawRay(this.transform.position, (this.transform.position - this.PrimaryTarget.transform.position).normalized * this.forwardMagnitude);
        }
    }

    void PrimaryMovementState()
    {
        int primaryMovementStateInt = StateHandler.CurrentState(ref this.timeInPrimaryMovementState, this.timeLimitForPrimaryMovementState, this.randPrimaryMovementPercentVal, this.PrimaryMovementStatePercentages, (int)EnemyStateData.EnemyPrimaryMovementState.Count);

        this.ePrimaryMovementState = (primaryMovementStateInt == -1) ? this.ePrimaryMovementState : (EnemyStateData.EnemyPrimaryMovementState)primaryMovementStateInt;
    }

    void SecondaryMovementState()
    {
        int secondaryMovementStateInt = StateHandler.CurrentState(ref this.timeInSecondaryMovementState, this.timeLimitForSecondaryMovementState, this.randSecondaryMovementPercentVal, this.SecondaryMovementStatePercentages, (int)EnemyStateData.EnemySecondaryMovementState.Count);

        this.eSecondaryMovementState = (secondaryMovementStateInt == -1) ? this.eSecondaryMovementState : (EnemyStateData.EnemySecondaryMovementState)secondaryMovementStateInt;
    }
}
