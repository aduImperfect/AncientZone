using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterDictionaryResult : MonoBehaviour
{
    [SerializeField]
    public EnemyStateData.EnemyPrimaryMovementFloatDictionary enemyPrimaryMovementDictionary;
    public IDictionary<EnemyStateData.EnemyPrimaryMovementState, float> EnemyPrimaryMovementDictionary
    {
        get { return this.enemyPrimaryMovementDictionary; }
        set { this.enemyPrimaryMovementDictionary.CopyFrom(value); }
    }

    [SerializeField]
    public EnemyStateData.EnemySecondaryMovementFloatDictionary enemySecondaryMovementDictionary;
    public IDictionary<EnemyStateData.EnemySecondaryMovementState, float> EnemySecondaryMovementDictionary
    {
        get { return this.enemySecondaryMovementDictionary; }
        set { this.enemySecondaryMovementDictionary.CopyFrom(value); }
    }

    public List<float> SpeedValueData;

    // Start is called before the first frame update
    void Start()
    {
        this.EnemyPrimaryMovementDictionary = new Dictionary<EnemyStateData.EnemyPrimaryMovementState, float>();
        this.EnemySecondaryMovementDictionary = new Dictionary<EnemyStateData.EnemySecondaryMovementState, float>();

        this.SpeedValueData = new List<float>();

        this.SpeedValueData.Add(0.0f);
        this.SpeedValueData.Add(5.0f);
        this.SpeedValueData.Add(10.0f);
        this.SpeedValueData.Add(30.0f);

        this.enemyPrimaryMovementDictionary.Add(EnemyStateData.EnemyPrimaryMovementState.Standing, this.SpeedValueData[0]);
        this.enemyPrimaryMovementDictionary.Add(EnemyStateData.EnemyPrimaryMovementState.Slow, this.SpeedValueData[1]);
        this.enemyPrimaryMovementDictionary.Add(EnemyStateData.EnemyPrimaryMovementState.Walk, this.SpeedValueData[2]);
        this.enemyPrimaryMovementDictionary.Add(EnemyStateData.EnemyPrimaryMovementState.Run, this.SpeedValueData[3]);
        
        this.enemySecondaryMovementDictionary.Add(EnemyStateData.EnemySecondaryMovementState.None, this.SpeedValueData[0]);
        this.enemySecondaryMovementDictionary.Add(EnemyStateData.EnemySecondaryMovementState.Strafing, this.SpeedValueData[1]);
    }

    // Update is called once per frame
    void Update()
    {
        BasicEnemyController enemyController = this.gameObject.GetComponent<BasicEnemyController>();

        NavMeshAgent meshAgent = this.gameObject.GetComponent<NavMeshAgent>();

        meshAgent.speed = this.EnemyPrimaryMovementDictionary[enemyController.ePrimaryMovementState];
        meshAgent.angularSpeed = this.EnemySecondaryMovementDictionary[enemyController.eSecondaryMovementState];
    }
}
