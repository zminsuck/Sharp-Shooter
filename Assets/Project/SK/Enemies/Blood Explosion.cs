using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 3;
    private void Start()
    {
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        // 오버랩 스피어를 이용해 모든 충돌 가속기 배열을 반환
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach(var hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();

            if (!playerHealth) continue;

            playerHealth.TakeDamage(damage);

            break;

            // playerHealth?.TakeDamage(damage);

            /*
            if (playerHealth)
            {
                playerHealth.TakeDamage(damage);
            }
            */
        }
    }
}
