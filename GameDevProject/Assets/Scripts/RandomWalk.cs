//Author: Adam Mills

using UnityEngine;
using UnityEngine.AI;

// Walk to a random position and repeat
[RequireComponent(typeof(NavMeshAgent))]
public class RandomWalk : MonoBehaviour
{
    public float m_Range = 25.0f;
    NavMeshAgent m_agent;

    //getting navmesh agent component for the person
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// update finds a new path for the person when the reach their destination
    /// </summary>
    void Update()
    {
        if (m_agent != null)
        {
            if (m_agent.pathPending || m_agent.remainingDistance > 0.1f)
                return;
            //setting destination to a random point in the range around them
            m_agent.destination = new Vector3(m_Range * Random.insideUnitCircle.x,0 , m_Range * Random.insideUnitCircle.y);

        }
    }

    //re-gets  the navMesh agent, resets the path
    public void ResetNavMesh()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }
}
