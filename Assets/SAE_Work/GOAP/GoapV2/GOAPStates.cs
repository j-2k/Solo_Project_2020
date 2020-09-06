using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPStates : MonoBehaviour
{
    List<string> baseStates;

    //vvvvvvvvvv COPY STATE STRINGS CONS vvvvvvvvvv
    public GOAPStates()
    {
        baseStates = new List<string>();
    }

    public GOAPStates(GOAPStates CopyString)
    {
        baseStates = new List<string>(CopyString.baseStates);
    }
    //^^^^^^^^^^ COPY STATE STRINGS CONS ^^^^^^^^^^

    public int CompareStates(GOAPStates subset)                                     //CompareStates function will be responsible for comparing the states of the agent and we will be checking
    {                                                                               //2 things at the start we will check the set and the subset, WHICH SET WE ARE CHECKING IS SUBSET.
        int difference = 0;                                                         //We have a difference int that will track how many differences / states cannot be done 
        for (int i = 0; i < subset.baseStates.Count; i++)                           //In this forloop we start checking through all the subsets
        {                                                                           //
            if (baseStates.Contains(subset.baseStates[i]) == false)                 //we check if the set contains subset if it does then we are fine IF WE DONT HAVE THE SUBSET IN THE SET
            {                                                                       //
                difference += 1;                                                    //we increment the difference by 1 and the difference will track the actions that we dont have in the set
            }                                                                       //
        }                                                                           //
        return difference;                                                          //then return the differences in the states.
    }                                                                               //

    public void AddUniqueStates(GOAPStates tobeAddedStates)
    {
        for (int i = 0; i < tobeAddedStates.baseStates.Count; i++)
        {
            if (baseStates.Contains(tobeAddedStates.baseStates[i]) == false)
            {
                baseStates.Add(tobeAddedStates.baseStates[i]);
            }
        }
    }

    public void RemoveUniqueStates(GOAPStates tobeRemovedStates)
    {
        for (int i = 0; i < tobeRemovedStates.baseStates.Count; i++)
        {
            baseStates.Remove(tobeRemovedStates.baseStates[i]);
        }
    }
}
