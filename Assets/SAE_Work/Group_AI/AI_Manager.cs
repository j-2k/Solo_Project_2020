using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : MonoBehaviour
{
    [SerializeField]GameObject childTL;
    [SerializeField]GameObject childTR;
    [SerializeField]GameObject childBR;
    [SerializeField]GameObject childBL;

    public enum AIStates
    {
        LeaderOnly,
        TL_Child,
        TR_Child,
        BR_Child,
        BL_Child
    }
    public AIStates currentAIStates;

    // Start is called before the first frame update
    void Start()
    {
        currentAIStates = AIStates.LeaderOnly;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentAIStates)
        {
            case AIStates.LeaderOnly:
                print("LeaderOnly phase, control the leader with WASD");
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += (transform.up * 10) * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position += -(transform.right * 10) * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.position += -(transform.up * 10) * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += (transform.right * 10) * Time.deltaTime;
                }
                //currentAIStates = AIStates.LeaderOnly;
                break;
            case AIStates.TL_Child:
                print("Top-Left Child now in check..." + " " +
                    "Press 1 to make it Follow the Leader" + " " +
                    "Press 2 to make it Roam around the area" + " " +
                    "Press 3 to make it Stay on its position");
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    childTL.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.FollowLeader;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    childTL.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.Roam;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    childTL.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.Stay;
                }
                //currentAIStates = AIStates.childTL;
                break;
            case AIStates.TR_Child:
                print("Top-Right Child now in check..." + " " +
                    "Press 1 to make it Follow the Leader" + " " +
                    "Press 2 to make it Roam around the area" + " " +
                    "Press 3 to make it Stay on its position");
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    childTR.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.FollowLeader;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    childTR.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.Roam;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    childTR.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.Stay;
                }
                //currentAIStates = AIStates.childTR;
                break;
            case AIStates.BR_Child:
                print("Bottom-Right Child now in check..." + " " +
                    "Press 1 to make it Follow the Leader" + " " +
                    "Press 2 to make it Roam around the area" + " " +
                    "Press 3 to make it Stay on its position");
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    childBR.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.FollowLeader;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    childBR.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.Roam;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    childBR.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.Stay;
                }
                //currentAIStates = AIStates.childBR;
                break;
            case AIStates.BL_Child:
                print("Bottom-Left Child now in check..." + " " +
                    "Press 1 to make it Follow the Leader" + " " +
                    "Press 2 to make it Roam around the area" + " " +
                    "Press 3 to make it Stay on its position");
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    childBL.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.FollowLeader;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    childBL.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.Roam;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    childBL.GetComponent<AI_Child>().currChildStates = AI_Child.AIChildStates.Stay;
                }
                //currentAIStates = AIStates.childBL;
                break;
        }
    }
}
