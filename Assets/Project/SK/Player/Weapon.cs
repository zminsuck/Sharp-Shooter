using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("����Ʈ �� ����")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource audioSource;        // �� �߻� �����
    [SerializeField] AudioClip gunShotClip;          // �� �߻� ����� Ŭ��
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();                         // �ѱ� ����Ʈ
        impulseSource.GenerateImpulse();            // ī�޶� ��鸲
        audioSource.PlayOneShot(gunShotClip);       // �� �߻� ����

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,
            out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);

            BossHealth bossHealth = hit.collider.GetComponent<BossHealth>();
            bossHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
