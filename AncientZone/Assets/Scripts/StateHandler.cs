using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int CurrentState(ref float timeInState, float maxLimitInState, int randVal, List<int> statePercentages, int statesCount)
    {
        if (timeInState > maxLimitInState)
        {
            for (int enumVal = 0; enumVal < statesCount; ++enumVal)
            {
                if (randVal < statePercentages[enumVal])
                {
                    timeInState = 0.0f;
                    return enumVal;
                }
            }
        }

        //Keep previous state!
        return -1;
    }
}
