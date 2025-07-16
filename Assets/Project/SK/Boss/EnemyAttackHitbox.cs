using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyAttackHitbox : MonoBehaviour
{
    [Tooltip("공격 시 줄 데미지")]
    public int damage = 1;

    [Tooltip("공격 대상이 될 레이어 (예: Player)")]
    public LayerMask targetLayer;

    void Awake()
    {
        // 실수로 isTrigger 설정 안 했을 경우 경고 출력
        Collider col = GetComponent<Collider>();
        if (!col.isTrigger)
        {
            Debug.LogWarning($"{name}의 Collider는 Trigger로 설정되어 있어야 합니다.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 대상이 지정된 레이어에 포함되는지 검사
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log($"{other.name}에게 데미지 {damage} 적용됨 (by {name})");
            }
        }
    }
}
