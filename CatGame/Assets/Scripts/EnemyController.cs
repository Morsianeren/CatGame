using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public List<Transform> roamingPath;
    public float investigationTime;
    public float walkingSpeed;
    public float runningSpeed;
    public NavMeshAgent agent;
    public Transform player;
    public float viewAngle = 120;
    public float viewDistance = 30;

    private Transform target;
    private enum states { IDLE, INVESTIGATING, ATTACKING};
    private states state;
    private bool forward;
    private float alertedAt;

    public void OnEnable()
    {
        EventManager.AlertActionEvent += AlertEnemy;
    }

    public void OnDisable()
    {
        EventManager.AlertActionEvent -= AlertEnemy;
    }
    void Start()
    {
        target = roamingPath[0];
        state = states.IDLE;
        agent.speed = walkingSpeed;
        agent.SetDestination(target.position);

    }

    void Update()
    {
        Debug.Log(state.ToString());
        var distance = Vector3.Distance(target.transform.position, transform.position);

        if (state == states.IDLE)
        {
            Debug.Log(forward);
            if (roamingPath.Find(i => i == target) != null)
            {
                Debug.Log("found " + roamingPath.FindIndex(i => i == target));
                if (distance < 4.1f)
                {
                    target = NextTarget();
                    agent.SetDestination(target.position);
                    Debug.Log(target.position.ToString());
                    Debug.Log(" in distance");
                }
            }

            if (playerInVision())
            {
                target = player;
                state = states.ATTACKING;
            }
        }
        else if (state == states.INVESTIGATING)
        {
            if (playerInVision())
            {
                state = states.ATTACKING;
            }

            if (Time.fixedTime - investigationTime >= alertedAt)
            {
                state = states.IDLE;
            }
        }
        if (state == states.ATTACKING)
        {
            if(playerInVision())
            {
                agent.SetDestination(target.position);
            }
            else
            {
                target = NextTarget();
                Debug.Log("target: " + target.position.ToString());
                foreach (var a in roamingPath)
                {
                    Debug.Log(a.position.ToString());
                }
                agent.SetDestination(target.position);
                state = states.IDLE;
            }

        }
    }

    public void AlertEnemy(Transform target)
    {
        this.target = target;
        state = states.INVESTIGATING;
        alertedAt = Time.fixedTime;
    }

    private bool playerInVision()
    {
        if (Vector3.Distance(transform.position, player.position) <= viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, viewDistance))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    Quaternion rotation = Quaternion.FromToRotation(transform.forward, directionToPlayer.normalized);
                    return (rotation.y >= -viewAngle/2 || rotation.y <= viewAngle/2);
                }
                return false;
            }
        }
        return false;
    }

    private Transform NextTarget()
    {
        int currentIndex = roamingPath.FindIndex(i => i == target);

        if(target == player.transform && state.Equals(states.ATTACKING))
        {
            return ClosestPoint();
        }

        if (currentIndex < roamingPath.Count - 1 && forward || (!forward && currentIndex == 0))
        {
            forward = true;
        }
        else
        {
            forward = false;
        }

        if (forward)
        {
            return roamingPath[currentIndex + 1];
        }
        else
        {
            return roamingPath[currentIndex - 1];
        }
    }

    private Transform ClosestPoint()
    {
        var closestDistanceToPoint = Vector3.Distance(transform.position, roamingPath[0].position);
        var closestPointIndex = 0;

        for (int i = 1; i < roamingPath.Count; i++)
        {
            var nextDistanceToPoint = Vector3.Distance(transform.position, roamingPath[i].position);

            if (nextDistanceToPoint < closestDistanceToPoint)
            {
                closestDistanceToPoint = nextDistanceToPoint;
                closestPointIndex = i;
            }
        }
        return roamingPath[closestPointIndex];
    }
}
