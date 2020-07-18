using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Child : MonoBehaviour
{
    [SerializeField] GameObject leaderObj;

    public enum AIChildStates
    {
        Roam,
        FollowLeader,
        Stay
    }
    public AIChildStates currChildStates;

    // Start is called before the first frame update
    void Start()
    {
        currChildStates = AIChildStates.FollowLeader;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currChildStates)
        {
            case AIChildStates.FollowLeader:
                print("Child FollowingLeader");
                transform.SetParent(leaderObj.transform);
                transform.LookAt(leaderObj.transform);
                if (Vector3.Distance(transform.position, leaderObj.transform.position) >= 3f)
                {
                    transform.position += (transform.forward * 5) * Time.deltaTime;
                }
                else
                {
                    transform.position += Vector3.zero;
                }
                break;
            case AIChildStates.Roam:
                print("Child Roaming");
                transform.SetParent(null);
                transform.Rotate(new Vector3(0, 2, 2), Space.Self);
                transform.position += -(transform.up * 5) * Time.deltaTime;
                break;
            case AIChildStates.Stay:
                print("Child Staying");
                transform.SetParent(null);
                break;
        }
    }
}
