using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_KillEnemy : GOAPGoal
{
    public override void GOAPStart()
    {
        agentsFinalGoal.AddNewState("DeadPlayer");
    }
}
