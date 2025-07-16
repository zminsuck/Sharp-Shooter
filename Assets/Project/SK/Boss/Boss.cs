using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Boss : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] string attackStateName = "Boss_Attack" +
        ""; // Animator ���� �̸�
    [SerializeField] float damageApplyTime = 0.4f; // �ִϸ��̼� ��� 40% ������ ������ ��

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

        // ���� ����
        if (distance <= attackRange && Time.time >= lastAttackTime)
        {
            agent.isStopped = true;
            animator.SetTrigger("IsAttacking");
            lastAttackTime = Time.time + attackCooldown;
            hasDealtDamage = false; // ���� ���� �ʱ�ȭ
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
                hasDealtDamage = true; // ������ �� ���� �ֵ��� ����
            }
        }
    }

    void DealDamage()
    {
        if (player == null) return;

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            Debug.Log("DealDamage(): ������ �����");
            health.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogWarning("PlayerHealth ������Ʈ ����");
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
