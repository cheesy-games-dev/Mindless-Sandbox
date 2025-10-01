using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINav : MonoBehaviour
{
    public float DetectionRange = 5f;
    public float Speed = 7.5f;
    public GameObject[] players;
    public NavMeshAgent agent;
    public Transform[] points;
    public string tagString = "Player";
    public Animator animator;

    private IEnumerator Start() {
        agent = GetComponent<NavMeshAgent>();
        Wander();
        yield return new WaitForSeconds(0.5f);
        agent.enabled = false;
    }

    void Update()
    {
        agent.enabled = true;
        players = GameObject.FindGameObjectsWithTag(tagString);

        GameObject target = null;

        // Set the target to the closest player
        if (players.Length > 0)
        {
            float minDistance = float.MaxValue;
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < DetectionRange && distance < minDistance)
                {
                    minDistance = distance;
                    target = player;
                }
            }


            if (target != null)
            {
                agent.speed = Speed;
                Chase(target.transform);
            }
        }

        // If we have a target, set the NavMeshAgent's destination to the target's position
        if (target != null)
        {
            agent.destination = target.transform.position;
        }
        else
        {
            agent.enabled = false;
            
        }
    }

    void Chase(Transform target)
    {
        animator.SetBool("Move", true);
        animator.speed = 1f;
        agent.destination = target.position;
        agent.updateRotation = true;
    }

    void Wander()
    {
        animator.SetBool("Move", true);
        if (points.Length == 0)
            return;

        int destPoint = Random.Range(0, points.Length);
        agent.destination = points[destPoint].position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}