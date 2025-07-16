using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] GameObject EnemyExpolosionVFX;

    [Header("Health")]
    [SerializeField] int startingHealth = 3;
    private int currentHealth;

    [Header("Ammo Drop")]
    [SerializeField] GameObject ammoPickupPrefab;
    [SerializeField] float ammoDropChance = 0.5f;

    private GameManager gameManager;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        TryDropItem(ammoPickupPrefab, ammoDropChance);

        Instantiate(EnemyExpolosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void TryDropItem(GameObject itemPrefab, float chance)
    {
        if (itemPrefab == null) return;

        if (Random.value <= chance)
        {
            Vector3 dropPosition = transform.position;
            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        }
    }
}
