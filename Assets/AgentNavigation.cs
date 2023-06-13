using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNavigation : MonoBehaviour
{
    
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform[] navigationPoints;
    [SerializeField] private int navigationPointsSize = 10;


    
    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        // find all navigation points in the scene with tag "wayPoint"
        GameObject[] wayPoints = GameObject.FindGameObjectsWithTag("wayPoint");
        // create a new array with the same size as the number of waypoints
        navigationPoints = new Transform[wayPoints.Length];
        // loop through all waypoints and add their transform to the navigationPoints array
        for (int i = 0; i < wayPoints.Length; i++)
        {
            navigationPoints[i] = wayPoints[i].transform;
        }
        // set the agent's destination to a random navigation point
        int randomIndex = Random.Range(0, navigationPoints.Length);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   void Update()
    {
        // if agent isn't moving, play idle animation and choose a new destination
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            animator.SetBool("isWalking", false);
            int randomIndex = Random.Range(0, navigationPoints.Length);
            UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
            UnityEngine.AI.NavMesh.CalculatePath(transform.position, navigationPoints[randomIndex].transform.position, UnityEngine.AI.NavMesh.AllAreas, path);
            agent.SetPath(path);
            animator.SetBool("isWalking", true);
}
}

    public void SetNavPoints(Transform[] navigationPoints) 
    {

    }
}