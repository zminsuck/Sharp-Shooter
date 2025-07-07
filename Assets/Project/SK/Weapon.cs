using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
    StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    void Update()
    {
        HandleShoot();
    }
    private void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damageAmount);

            Debug.Log(hit.collider.name);

            starterAssetsInputs.ShootInput(false);
        }
    }
}
