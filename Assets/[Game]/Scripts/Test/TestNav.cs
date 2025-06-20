using UnityEngine;
using UnityEngine.AI;

public class TestNav : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target= GameObject.FindWithTag("Base")?.transform; 

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("Hedef (target) atanmadı.");
        }
    }

    private void Update()
    {
        if (target != null && agent.destination != target.position)
        {
            agent.SetDestination(target.position);
        }
    }
}