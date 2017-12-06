﻿//Author: Adam Mills

using UnityEngine;
using UnityEngine.AI;

// Walk to a random position and repeat
[RequireComponent(typeof(NavMeshAgent))]
public class RandomWalk : MonoBehaviour
{
    public float m_Range = 25.0f;
    NavMeshAgent m_agent;

    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (m_agent != null)
        {
            if (m_agent.pathPending || m_agent.remainingDistance > 0.1f)
                return;

            m_agent.destination = new Vector3(m_Range * Random.insideUnitCircle.x,0 , m_Range * Random.insideUnitCircle.y);

        }
    }

    public void ResetNavMesh()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }
}
