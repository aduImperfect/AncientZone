using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateData : MonoBehaviour
{
    public enum EnemyPrimaryMovementState
    {
        Standing = 0,
        Slow,
        Walk,
        Run,
        Count,
    }

    public enum EnemySecondaryMovementState
    {
        Standing = 0,
        Strafing,
        Count,
    }

    public enum EnemyPrimaryAttackState
    {
        AxeSwing = 0,
        AxeSlam,
        AxeDoubleSwing,
        Count,
    }

    [Serializable]
    public class EnemyPrimaryMovementFloatDictionary : SerializableDictionary<EnemyPrimaryMovementState, float> { }

    [Serializable]
    public class EnemySecondaryMovementFloatDictionary : SerializableDictionary<EnemySecondaryMovementState, float> { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
