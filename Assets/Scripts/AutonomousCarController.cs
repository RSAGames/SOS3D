using UnityEngine;
using UnityEngine.AI;

public class AutonomousCarController : MonoBehaviour
{
    public string roadTag = "CarWay";
    private NavMeshAgent navMeshAgent;
    private Vector3 currentDestination;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomDestinationOnRoad();
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            SetRandomDestinationOnRoad();
        }
    }

    private void SetRandomDestinationOnRoad()
    {
        GameObject[] roadObjects = GameObject.FindGameObjectsWithTag(roadTag);

        if (roadObjects.Length > 0)
        {
            GameObject randomRoad = roadObjects[Random.Range(0, roadObjects.Length)];
            Vector3 randomPoint = randomRoad.transform.position + Random.insideUnitSphere * 10f;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                currentDestination = hit.position;

                if (NavMesh.CalculatePath(transform.position, currentDestination, NavMesh.AllAreas, new NavMeshPath()))
                {
                    navMeshAgent.SetDestination(currentDestination);
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