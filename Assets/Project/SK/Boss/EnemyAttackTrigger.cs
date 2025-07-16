using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    private Boss boss;

    void Awake()
    {
        boss = GetComponentInParent<Boss>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.TryAttack(); // 공격 시도
        }
    }
}
