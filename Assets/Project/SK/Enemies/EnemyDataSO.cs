using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    public float moveSpeed = 3f;
    public int maxHealth = 3;
    public int attackDamage = 10;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public GameObject deathVFX;
}
