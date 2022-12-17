using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentLinkMover : MonoBehaviour
{
    IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        while (true)
        {

            if (agent.isOnOffMeshLink)
            {
                //Debug.Log("On link!");

                yield return StartCoroutine(OpenDoor(agent));
                agent.CompleteOffMeshLink();
            }
            else
            {
                //Debug.Log("Not on link");

            }
            yield return null;
        }
    }

    IEnumerator OpenDoor(NavMeshAgent agent)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;

        //Debug.Log("Check 1");

        DoorMeshLink doorMeshLink = data.offMeshLink.GetComponent<DoorMeshLink>();
        if (doorMeshLink != null)
        {
            //Debug.Log("Check 2");

            doorMeshLink.OpenDoor();
        }
        else
        {
            Debug.LogError($"DoorMeshLink not found on {doorMeshLink.gameObject.name}");
        }

        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }

        doorMeshLink.CloseDoor();
    }
}
