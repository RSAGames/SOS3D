using UnityEngine;

public class DyingAnimation : MonoBehaviour
{
    public float detectionRange = 5f;
    public Animator animator;
    public Transform player;

    private bool playerInRange;

    private void Update()
    {
        // Calculate the distance between the player and the object
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange)
        {
        
            animator.SetBool("Standing", false);
            animator.SetBool("isLying", true);
        }
    }
}
