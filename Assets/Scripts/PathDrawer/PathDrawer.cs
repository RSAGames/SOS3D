using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class PathDrawer : MonoBehaviour
{
    public Transform player;
    public Transform targetObject; // Reference to the target object
    public LineRenderer lineRenderer; // Reference to the Line Renderer component


    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the Line Renderer component
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Check if both  player and targetObject is on the NavMesh
        if (NavMesh.SamplePosition(player.position, out NavMeshHit hit1, 0.1f, NavMesh.AllAreas) &&
            NavMesh.SamplePosition(targetObject.position, out NavMeshHit hit2, 0.1f, NavMesh.AllAreas))
        {
            Debug.Log("Both on NavMesh");
        }
        else
        {
            // if player is not on the NavMesh, Debug a warning message
            if (!NavMesh.SamplePosition(player.position, out NavMeshHit hit3, 0.1f, NavMesh.AllAreas))
            {
                Debug.LogWarning("Player is not on the NavMesh");
            }
            if (!NavMesh.SamplePosition(targetObject.position, out NavMeshHit hit4, 0.1f, NavMesh.AllAreas))
            {
                Debug.LogWarning("Target is not on the NavMesh");
            }
        }


        // Calculate a path from the player's position to the target object
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        UnityEngine.AI.NavMesh.CalculatePath(player.position, targetObject.position, UnityEngine.AI.NavMesh.AllAreas, path);

        // Update the Line Renderer with the calculated path
        lineRenderer.positionCount = path.corners.Length;
        Debug.Log(path.corners.Length);
        for (int i = 0; i < path.corners.Length; i++)
        {
            Debug.Log(path.corners[i]);
            lineRenderer.SetPosition(i, path.corners[i]);
        }
    }
}