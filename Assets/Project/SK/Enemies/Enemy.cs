using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected FirstPersonController player;
    protected NavMeshAgent agent;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    protected virtual void Update()
    {
        if (!player) return;
        agent.SetDestination(player.transform.position);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<EnemyHealth>()?.SelfDestruct();
        }
    }
}
