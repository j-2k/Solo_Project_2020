using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapActionString : MonoBehaviour
{
    public string actionName;
    public List<string> outcomeStates;
    public List<string> requiredStates;

    public virtual void RunActionString(GOAPAgentString agent)
    {
        Debug.Log(actionName);
    }
}
