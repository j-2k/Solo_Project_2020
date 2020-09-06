using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPAgent : MonoBehaviour
{//s lifo q fifo
    List<GOAPAction> agentActions;
    Queue<GOAPAction> currPlan;
    List<GOAPGoal> agentGoals;
    public GOAPStates worldStates;
    //statesc @ 20.35
    [SerializeField] int planDepth;

    [SerializeField] float speed;
    GOAPAction currentAction;
    //vari @1:31:18


    //27.56 con ifs
    void Start()
    {
        agentActions = new List<GOAPAction>(GetComponents<GOAPAction>());
        agentGoals = new List<GOAPGoal>(GetComponents<GOAPGoal>());
        currPlan = new Queue<GOAPAction>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Plan();                                    //now we start planning throughout the tree (ONLY PLANNING NO ACTIONS YET)
            while (currPlan.Count != 0)                     //as long as we have a plan keep running the agents actions
            {
                currPlan.Dequeue().RunAction(this);         //here we dequeue & run the action (if by any chance there is no action left or something fails we force a NEW plan)
            }
        }
    }

    public void Plan()
    {
        currPlan.Clear();                                               //first of all clear our current plan
                                                                        //now start making stacks for the planning tree
        Stack<GOAPStates> goapSimWorldStates = new Stack<GOAPStates>();    //first stack is for the world states
        Stack<GOAPAction> goapSimActions = new Stack<GOAPAction>();        //second stack will hold the actions
        Stack<int> goapSimTreeDepth = new Stack<int>();                    //third stack will hold the depth of the tree

        GOAPAction[] agentSimPlans = new GOAPAction[planDepth];            //have a plan that will go as deep as planDepth variable we set above

        int lowestDepth = int.MaxValue;                                 //set the lowest tree depth to infinity because we dont know the best plan as of yet so we just set the lowest depth to be inf

        //START INITIALIZING BASE VALUES FOR OUR STACKS
        goapSimWorldStates.Push(new GOAPStates(worldStates));               //push the present real world state for the agent
        goapSimTreeDepth.Push(0);                                          //push 0 depth because we just are starting the plan so when initializng we are just at 0
        goapSimActions.Push(null);                                         //here we push null because we again we just starting the plan and we have no actions yet but we push null to make sure the agent moves together with the other stacks

        //NOW BEGIN THE PLANNING LOOP, now we will keep planning and find ALL the tree nodes UNTILL we find everything from the present to then finally pop the actual present node from the stack 
        //ONCE we have popped the present node this means we have bascially reached the end and have information on all the nodes
        while (goapSimWorldStates.Count != 0)
        {
            GOAPStates agentCurrentSimState = goapSimWorldStates.Pop();    //now we pop the first world state / node and then we begin going through its children nodes
            int agentCurrentDepth = goapSimTreeDepth.Pop();                //get the depth for this world state
            GOAPAction agentCurrentAction = goapSimActions.Pop();          //get the actions that have led us to this current world state
            agentSimPlans[agentCurrentDepth] = agentCurrentAction;         //insert our agents current action in an array so we know what we have gone through

            //check if i reached the goal or the max planned depth
            if (agentCurrentSimState.CompareStates(agentGoals[0].finalState) == 0 || agentCurrentDepth >= planDepth)
            {
                if (agentCurrentDepth < lowestDepth)
                {
                    currPlan.Clear();
                    for (int i = 0; i < agentSimPlans.Length; i++)
                    {
                        if (agentSimPlans[i] != null)
                        {
                            currPlan.Enqueue(agentSimPlans[i]);
                        }
                    }
                }
            }
            else//check all the available actions for the next step
            {
                for (int i = 0; i < agentActions.Count; i++)
                {   //check if the required states EXIST in the agents current simulation state         //check if the actions outcome states is greater than 0 making sure it is not going to be redundant
                    //making sure there is nothing left out of the required states in the c world state //we do this by checking the current sim world state if it has all the outcomes so if its > 0 something is new / this action is not redundant
                    if (agentCurrentSimState.CompareStates(agentActions[i].requiredStates) == 0 && agentCurrentSimState.CompareStates(agentActions[i].outcomeStates) > 0)
                    {
                        //start by creating a new branch & essentially start a new action
                        GOAPStates newSimState = new GOAPStates(agentCurrentSimState);
                        newSimState.AddUniqueStates(agentActions[i].outcomeStates);
                        goapSimWorldStates.Push(newSimState);
                        goapSimTreeDepth.Push(agentCurrentDepth + 1);
                        goapSimActions.Push(agentActions[i]);
                    }
                }
            }
        }
    }
}
