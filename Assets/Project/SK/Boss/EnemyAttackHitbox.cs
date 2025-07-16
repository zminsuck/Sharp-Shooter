using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyAttackHitbox : MonoBehaviour
{
    [Tooltip("���� �� �� ������")]
    public int damage = 1;

    [Tooltip("���� ����� �� ���̾� (��: Player)")]
    public LayerMask targetLayer;

    void Awake()
    {
        // �Ǽ��� isTrigger ���� �� ���� ��� ��� ���
        Collider col = GetComponent<Collider>();
        if (!col.isTrigger)
        {
            Debug.LogWarning($"{name}�� Collider�� Trigger�� �����Ǿ� �־�� �մϴ�.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ����� ������ ���̾ ���ԵǴ��� �˻�
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log($"{other.name}���� ������ {damage} ����� (by {name})");
            }
        }
    }
}
