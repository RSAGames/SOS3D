using UnityEngine;
using UnityEngine.AI;

public class AutonomousCarController : MonoBehaviour
{
    public string roadTag = "CarWay";
    private NavMeshAgent navMeshAgent;
    public Transform currentDestination;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestinationOnRoad();
    }

    private void Update()
    {
        if (navMeshAgent != null && !navMeshAgent.isOnNavMesh)
        {
            PlaceAgentOnNavMesh();
        }
        else if (navMeshAgent != null && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            SetDestinationOnRoad();
        }
        else if (navMeshAgent == null)
        {
            Debug.Log("NavMeshAgent is null");
        }
    }

    private void PlaceAgentOnNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 100f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            Debug.Log("Agent is now on the NavMesh!");
            SetDestinationOnRoad();
        }
        else
        {
            Debug.Log("No valid position on the NavMesh!");
        }
    }

    private void SetDestinationOnRoad()
    {
        GameObject[] roadObjects = GameObject.FindGameObjectsWithTag(roadTag);

        if (roadObjects.Length > 0)
        {
            GameObject randomRoad = roadObjects[Random.Range(0, roadObjects.Length)];
            Vector3 randomPoint = randomRoad.transform.position + Random.insideUnitSphere * 10f;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                currentDestination.position = hit.position;

                if (NavMesh.CalculatePath(transform.position, currentDestination.position, NavMesh.AllAreas, new NavMeshPath()))
                {
                    navMeshAgent.SetDestination(currentDestination.position);
                }
                else
                {
                    Debug.Log("No valid path to the destination!");
                }
            }
            else
            {
                Debug.Log("Random point is outside the NavMesh bounds!");
            }
        }
    }
}
