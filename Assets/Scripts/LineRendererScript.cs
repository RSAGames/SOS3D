using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    public Transform targetObject; // Reference to the target object
    private Vector3 offset = new Vector3(0, 1f, 0); // Offset the line renderer so it is above the ground
    public int segmentCount = 5; // Number of line segments

    private LineRenderer lineRenderer; // Reference to the Line Renderer component

    private void Start()
    {
        // Get the Line Renderer component
        lineRenderer = GetComponent<LineRenderer>();

        // Set the number of positions in the line renderer
        lineRenderer.positionCount = segmentCount + 1;
    }

    private void Update()
    {
        // Update the positions for the line renderer
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount; // Calculate the position interpolation value
            Vector3 position = Vector3.Lerp(transform.position, targetObject.position, t); // Interpolate the position
            lineRenderer.SetPosition(i, position+offset); // Set the position in the line renderer
        }
    }
}




