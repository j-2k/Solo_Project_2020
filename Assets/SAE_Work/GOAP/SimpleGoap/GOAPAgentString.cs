using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPAgentString : MonoBehaviour
{
    public List<string> agentGoals;
    public List<string> worldState;
    List<GoapActionString> agentActions;
    Stack<GoapActionString> currPlan;

    // Start is called before the first frame update
    void Start()
    {
        agentActions = new List<GoapActionString>(GetComponents<GoapActionString>());
        currPlan = new Stack<GoapActionString>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AgentPlanning();                                //now we start planning throughout the tree (ONLY PLANNING NO ACTIONS YET)
            while (currPlan.Count != 0)                     //as long as we have a plan keep running the agents actions
            {
                currPlan.Pop().RunActionString(this);             //here we start & run the action (if by any chance there is no action left or something fails we force a NEW plan)
            }
        }
    }

    public void AgentPlanning()
    {
        currPlan.Clear();
        List<string> newTargetStates = new List<string>(agentGoals);
        while(CompareStates(worldState,newTargetStates) > 0)    //as long as the new target state is not contained in the world states we keep on running this while loop
        {
            GoapActionString bestAction = GetBestAction(newTargetStates);
            currPlan.Push(bestAction);
            RemoveUniqueStates(newTargetStates, bestAction.outcomeStates);
            AddUniqueStates(newTargetStates, bestAction.requiredStates);
        }
    }

    int CompareStates (List<string> set, List<string> subset)   //CompareStates function will be responsible for comparing the states of the agent and we will be checking
    {                                                           //2 things at the start we will check the set and the subset, WHICH SET WE ARE CHECKING IS SUBSET.
        int difference = 0;                                     //We have a difference int that will track how many differences / states cannot be done 
        for (int i = 0; i < subset.Count; i++)                  //In this forloop we start checking through all the subsets
        {                                                       //
            if (set.Contains(subset[i]) == false)               //we check if the set contains subset if it does then we are fine IF WE DONT HAVE THE SUBSET IN THE SET
            {                                                   //
                difference += 1;                                //we increment the difference by 1 and the difference will track the actions that we dont have in the set
            }                                                   //
        }                                                       //
        return difference;                                      //then return the differences in the states.
    }                                                           //

    void AddUniqueStates(List<string> addStates , List<string> tobeAddedStates)
    {
        for (int i = 0; i < tobeAddedStates.Count; i++)
        {
            if (addStates.Contains(tobeAddedStates[i]) == false)
            {
                addStates.Add(tobeAddedStates[i]);
            }
        }
    }

    void RemoveUniqueStates(List<string> removeStates , List<string> tobeRemovedStates)
    {
        for (int i = 0; i < tobeRemovedStates.Count; i++)
        {
            removeStates.Remove(tobeRemovedStates[i]);
        }
    }

    GoapActionString GetBestAction (List<string> targetStates)
    {
        int minIndex = -1; //n1
        int minValue = int.MaxValue;
        for (int i = 0; i < agentActions.Count; i++)
        {
            int currentDifference = CompareStates(agentActions[i].outcomeStates, targetStates);
            if (currentDifference < minValue)
            {
                minIndex = i;
                minValue = currentDifference;
            }
        }
        return agentActions[minIndex];
    }
}
