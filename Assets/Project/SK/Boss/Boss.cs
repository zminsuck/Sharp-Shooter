using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Boss : MonoBehaviour
{
    [Header("공격 설정")]
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] string attackStateName = "Boss_Attack" +
        ""; // Animator 상태 이름
    [SerializeField] float damageApplyTime = 0.4f; // 애니메이션 재생 40% 시점에 데미지 줌

    Animator animator;
    NavMeshAgent agent;
    Transform player;
    float lastAttackTime;
    bool hasDealtDamage;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null || agent == null || animator == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 공격 조건
        if (distance <= attackRange && Time.time >= lastAttackTime)
        {
            agent.isStopped = true;
            animator.SetTrigger("IsAttacking");
            lastAttackTime = Time.time + attackCooldown;
            hasDealtDamage = false; // 공격 시점 초기화
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }

        animator.SetBool("IsMoving", !agent.isStopped && agent.velocity.magnitude > 0.1f);

        HandleDamageTiming();
    }

    void HandleDamageTiming()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(attackStateName))
        {
            if (!hasDealtDamage && stateInfo.normalizedTime >= damageApplyTime)
            {
                DealDamage();
                hasDealtDamage = true; // 데미지 한 번만 주도록 설정
            }
        }
    }

    void DealDamage()
    {
        if (player == null) return;

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            Debug.Log("DealDamage(): 데미지 적용됨");
            health.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogWarning("PlayerHealth 컴포넌트 없음");
        }
    }

    public void TryAttack()
    {
        if (Time.time >= lastAttackTime)
        {
            animator.SetTrigger("IsAttacking");
            lastAttackTime = Time.time + attackCooldown;
        }
    }


}
