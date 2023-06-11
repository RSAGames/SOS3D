using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    // This script is used to control the agent's movement and animation
    // It is attached to the agent's root object
    // It requires a NavMeshAgent and Animator component

    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] wayPoints;
    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        wayPoints = GameObject.FindGameObjectsWithTag("wayPoint");
    }

    public void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    public void Resume()
    {
        agent.isStopped = false;
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }

    public void SetAnimation(string animation)
    {
        animator.Play(animation);
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.speed = speed;
    }

    public void SetAnimationBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void SetAnimationTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void SetAnimationFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }

    public void SetAnimationInt(string name, int value)
    {
        animator.SetInteger(name, value);
    }

    public void SetAnimationLayerWeight(int layer, float weight)
    {
        animator.SetLayerWeight(layer, weight);
    }

    public void SetAnimationLayerWeight(string layerName, float weight)
    {
        animator.SetLayerWeight(animator.GetLayerIndex(layerName), weight);
    }


    // Start is called before the first frame update
    void Start()
    {
        // Random agent speed and destination
        agent.speed = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // if agent isn't moving, play idle animation and choose a new destination
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            animator.SetBool("isWalking", false);
            int randomIndex = Random.Range(0, wayPoints.Length);
            agent.SetDestination(wayPoints[randomIndex].transform.position);
            animator.SetBool("isWalking", true);
}
}
}