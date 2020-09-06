using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPAction : MonoBehaviour
{
    public string agentAction;
    public GOAPStates outcomeStates;
    public GOAPStates requiredStates;

    void Start()
    {//aa=th.gt.nm
        GOAPStart();
    }

    public abstract void GOAPStart();

    public abstract bool RunAction(GOAPAgent agent);

}
