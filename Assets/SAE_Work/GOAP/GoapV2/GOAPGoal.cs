using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPGoal : MonoBehaviour
{
    string finalGoal;
    public GOAPStates agentsFinalGoal;

    private void Start()
    {
        finalGoal = GetType().Name;
        GOAPStart();
    }

    public abstract void GOAPStart();
}
