using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPGoal : MonoBehaviour
{
    string finalGoal;
    public GOAPStates finalState;

    private void Start()
    {
        GOAPStart();
    }

    public abstract bool GOAPStart();

    public abstract bool ValidGoal(GOAPAgent agent);
}
