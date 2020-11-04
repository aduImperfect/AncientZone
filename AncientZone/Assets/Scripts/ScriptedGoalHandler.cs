using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptedGoalHandler : MonoBehaviour
{
    public List<Vector3> Goals;
    public List<float> GoalsAuraRanges;
    public List<float> AngleToGoals;

    public List<Vector3> castHitPointsList;
    public List<Vector3> castHitPointsDirList;

    public float minAuraRange;
    public float maxAuraRange;

    public int minNumGoals;
    public int maxNumGoals;
    public int numGoals;

    public float minMagnitude;
    public float maxMagnitude;
    public float goalMagnitude;

    public float minXLoc;
    public float maxXLoc;

    public float minZLoc;
    public float maxZLoc;

    public int currentGoalIndex;

    public float timeLimitForGoalPositionState;

    public float timeInGoalPositionState;

    public Transform characterTransform;

    public float closenessToCurrentGoal;

    public float minTimeLimitInState;
    public float maxTimeLimitInState;

    public bool isRandomized;

    public GameObject PrefabDebugCube;

    public NavMeshPath meshPath;

    public float timeSinceLastGoalReached;

    public float maxTimeWandering;

    // Start is called before the first frame update
    void Start()
    {
        this.currentGoalIndex = 0;
        this.characterTransform = this.gameObject.transform;
        this.AngleToGoals = new List<float>();

        this.meshPath = new NavMeshPath();

        this.Goals = new List<Vector3>();

        this.GoalsAuraRanges = new List<float>();

        this.numGoals = Random.Range(this.minNumGoals, this.maxNumGoals);

        this.castHitPointsList = VertexCheckers.GetCastHitPoints(VertexCheckers.SweepAround(this.transform));

        this.castHitPointsDirList = new List<Vector3>();

        this.timeSinceLastGoalReached = 0.0f;

        for (int dirIdx = 0; dirIdx < this.castHitPointsList.Count; ++dirIdx)
        {
            this.castHitPointsDirList.Add(this.castHitPointsList[dirIdx] - this.transform.position);
        }

        for (int jdx = 0; jdx < this.numGoals; ++jdx)
        {
            this.GoalsAuraRanges.Add(Random.Range(minAuraRange, maxAuraRange));
        }

        for (int idx = 0; idx < this.numGoals; ++idx)
        {
            this.Goals.Add(this.CalculateNewVectorPoint(idx));
        }

        for (int indx = 0; indx < Goals.Count; ++indx)
        {
            this.AngleToGoals.Add(0.0f);

            this.PrefabDebugCube.transform.position = Goals[indx];
            //Instantiate(PrefabDebugCube);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.timeSinceLastGoalReached += Time.deltaTime;

        if (this.isRandomized)
        {
            this.GoalStateRandomized();
        }
        else
        {
            this.GoalStateStepped();
        }

        this.DrawTargetToGoalLines();
        this.CalculateAngleToGoals();
    }

    public void GoalStateStepped()
    {
        this.timeLimitForGoalPositionState = Random.Range(this.minTimeLimitInState, this.maxTimeLimitInState);

        Vector3 differenceInPos = this.characterTransform.position - this.Goals[this.currentGoalIndex];

        if (Mathf.Abs(differenceInPos.magnitude) < this.closenessToCurrentGoal)
        {
            this.timeInGoalPositionState += Time.deltaTime;
            if (this.timeInGoalPositionState > this.timeLimitForGoalPositionState)
            {
                if (this.currentGoalIndex == (this.Goals.Count - 1))
                {
                    this.currentGoalIndex = 0;
                }
                else
                {
                    ++this.currentGoalIndex;
                }
                this.timeInGoalPositionState = 0.0f;
                this.timeSinceLastGoalReached = 0.0f;
            }
        }
        else if (this.timeSinceLastGoalReached > this.maxTimeWandering)
        {
            //This goal is no longer required!!
            this.Goals.RemoveAt(this.currentGoalIndex);
            this.GoalsAuraRanges.RemoveAt(this.currentGoalIndex);
            this.AngleToGoals.RemoveAt(this.currentGoalIndex);

            this.GoalsAuraRanges.Add(Random.Range(minAuraRange, maxAuraRange));
            this.Goals.Add(this.CalculateNewVectorPoint(this.Goals.Count));
            this.AngleToGoals.Add(0.0f);

            if (this.currentGoalIndex == (this.Goals.Count - 1))
            {
                this.currentGoalIndex = 0;
            }
            else
            {
                ++this.currentGoalIndex;
            }
            this.timeInGoalPositionState = 0.0f;
            this.timeSinceLastGoalReached = 0.0f;
        }
    }

    public void GoalStateRandomized()
    {
        this.timeLimitForGoalPositionState = Random.Range(this.minTimeLimitInState, this.maxTimeLimitInState);

        Vector3 differenceInPos = this.characterTransform.position - this.Goals[this.currentGoalIndex];

        if (Mathf.Abs(differenceInPos.magnitude) < this.closenessToCurrentGoal)
        {
            this.timeInGoalPositionState += Time.deltaTime;
            if (this.timeInGoalPositionState > this.timeLimitForGoalPositionState)
            {
                this.currentGoalIndex = Random.Range(0, this.Goals.Count - 1);
                this.timeInGoalPositionState = 0.0f;
                this.timeSinceLastGoalReached = 0.0f;
            }
        }
        else if (this.timeSinceLastGoalReached > this.maxTimeWandering)
        {
            //This goal is no longer required!!
            this.Goals.RemoveAt(this.currentGoalIndex);
            this.GoalsAuraRanges.RemoveAt(this.currentGoalIndex);
            this.AngleToGoals.RemoveAt(this.currentGoalIndex);

            this.GoalsAuraRanges.Add(Random.Range(minAuraRange, maxAuraRange));
            this.Goals.Add(this.CalculateNewVectorPoint(this.Goals.Count));
            this.AngleToGoals.Add(0.0f);

            this.currentGoalIndex = Random.Range(0, this.Goals.Count - 1);
            this.timeInGoalPositionState = 0.0f;
            this.timeSinceLastGoalReached = 0.0f;
        }
    }

    void DrawTargetToGoalLines()
    {
        foreach (Vector3 goalPos in Goals)
        {
            Debug.DrawLine(this.characterTransform.position, goalPos);
        }
    }

    void CalculateAngleToGoals()
    {
        for (int goalIdx = 0; goalIdx < this.Goals.Count; ++goalIdx)
        {
            Vector3 characterForward = this.characterTransform.TransformDirection(Vector3.forward);

            Vector3 toTarget = this.Goals[goalIdx] - this.characterTransform.position;

            float angleToTarget = Vector3.Dot(characterForward.normalized, toTarget.normalized);

            float angleInDegToTarget = Mathf.Acos(angleToTarget) * Mathf.Rad2Deg;

            this.AngleToGoals[goalIdx] = angleInDegToTarget;
        }
    }

    Vector3 CalculateNewVectorPoint(int index)
    {
        this.goalMagnitude = Random.Range(this.minMagnitude, this.maxMagnitude);

        Vector3 characterPos = this.gameObject.transform.position;

        Vector3 newPos = new Vector3(characterPos.x + Random.Range(this.minXLoc, this.maxXLoc), characterPos.y, characterPos.z + Random.Range(this.minZLoc, this.maxZLoc));

        Vector3 characterGoalDist = newPos - characterPos;

        bool isPathValid = NavMesh.CalculatePath(characterPos, newPos, NavMesh.AllAreas, this.meshPath);

        int kIndex = 0;
        while ((characterGoalDist.magnitude > this.goalMagnitude) || (!isPathValid) || ((index != 0) && (kIndex < index) && ((newPos - this.Goals[kIndex]).magnitude < this.GoalsAuraRanges[kIndex++])))
        {
            newPos = new Vector3(characterPos.x + Random.Range(this.minXLoc, this.maxXLoc), characterPos.y, characterPos.z + Random.Range(this.minZLoc, this.maxZLoc));
            characterGoalDist = newPos - characterPos;
            isPathValid = NavMesh.CalculatePath(characterPos, newPos, NavMesh.AllAreas, this.meshPath);
            kIndex = 0;
        }

        for (int dirIdx = 0; dirIdx < this.castHitPointsDirList.Count; ++dirIdx)
        {
            Vector3 normalizedCastPointDir = this.castHitPointsDirList[dirIdx].normalized;

            Vector3 normalizedNewPosDir = characterGoalDist.normalized;

            int normNewXVal = (int)(normalizedNewPosDir.x * 100);
            int normNewZVal = (int)(normalizedNewPosDir.z * 100);

            int normCastPtXVal = (int)(normalizedCastPointDir.x * 100);
            int normCastPtZVal = (int)(normalizedCastPointDir.z * 100);

            if((normNewXVal != normCastPtXVal) || (normNewZVal != normCastPtZVal))
            {
                continue;
            }

            if(characterGoalDist.magnitude > this.castHitPointsDirList[dirIdx].magnitude)
            {
                newPos = this.castHitPointsList[dirIdx];
                characterGoalDist = newPos - characterPos;
            }
        }

        return newPos;
    }
}
