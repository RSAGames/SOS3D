using UnityEngine;

public class DyingAnimation : MonoBehaviour
{
    public float detectionRange = 5f;
    public GameObject player;
    public Animator animator;
    private bool isLying = false;

    private void Update()
    {
        if (player != null)
        {
            // Calculate the distance between the object and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Check if the player is within the detection range
            if (distanceToPlayer <= detectionRange)
            {
                animator.SetBool("Standing", false);
                animator.SetBool("isLying", true);
            }
        }
    }
}
