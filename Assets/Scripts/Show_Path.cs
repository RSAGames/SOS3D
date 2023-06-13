// ShowGoldenPath
using UnityEngine;
using UnityEngine.AI;

public class Show_Path : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform player;
    private NavMeshPath path;
    [SerializeField]
    private float dotSpacing = 0.5f;
    [SerializeField]
    private Vector3 route_offset = new Vector3(0, 1.5f, 2); // Offset the line renderer so it is above the ground
    private float elapsed = 0.0f;
    [SerializeField] private float refresh_time = 0.25f;
    [SerializeField] private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.textureMode = LineTextureMode.Tile;
        path = new NavMeshPath();
        elapsed = 0.0f;
        CalculateAndSetLinePositions();

    }

    void Update()
    {
        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > refresh_time)
        {
                   CalculateAndSetLinePositions();
                   elapsed = 0.0f;
        }
    }

        private void CalculateAndSetLinePositions()
    {
        // Calculate a path from the player's position to the target object using NavMesh
        path = new NavMeshPath();
        NavMesh.CalculatePath(player.position, target.position, NavMesh.AllAreas, path);

        // Calculate the total length of the path
        float pathLength = 0f;
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            pathLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        // Calculate the number of dots needed on the path
        int dotCount = Mathf.CeilToInt(pathLength / dotSpacing);

        // Calculate the distance between each dot
        float segmentLength = pathLength / dotCount;

        // Set the positions and gaps for the line renderer
        lineRenderer.positionCount = dotCount;
        for (int i = 0; i < dotCount; i++)
        {
            // Calculate the position of each dot along the path
            float distanceAlongPath = i * segmentLength;
            Vector3 position = GetPointAlongPath(path, distanceAlongPath) + route_offset;

            // Set the position in the line renderer
            lineRenderer.SetPosition(i, position + player.forward * 500f);

            // Set the gap between each dot
            lineRenderer.SetPosition(i, position + (lineRenderer.GetPosition(i) - position).normalized * segmentLength);
        }
    }

    private Vector3 GetPointAlongPath(NavMeshPath path, float distance)
    {
        float currentDistance = 0f;
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            float segmentLength = Vector3.Distance(path.corners[i], path.corners[i + 1]);
            if (currentDistance + segmentLength >= distance)
            {
                float t = (distance - currentDistance) / segmentLength;
                return Vector3.Lerp(path.corners[i], path.corners[i + 1], t);
            }
            currentDistance += segmentLength;
        }
        return path.corners[path.corners.Length - 1];
    }
}