using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_GetMeleeItem : GOAPAction
{
    public Transform meleeItem;

    public override void GOAPStart()
    {
        outcomeStates.AddNewState("WeildingMelee");
    }

    public override bool RunAction(GOAPAgent agent)
    {
        agent.transform.LookAt(meleeItem);
        agent.transform.position += agent.transform.forward * 2;
        if (Vector3.Distance(agent.transform.position,meleeItem.transform.position) < 0.25f)
        {
            bool one = false;
            if (one == false)
            {
                meleeItem.transform.position = agent.transform.position + agent.transform.right * 1;
                meleeItem.transform.SetParent(agent.transform);
                one = true;
                return true;
            }
        }
        return true;
    }
}
