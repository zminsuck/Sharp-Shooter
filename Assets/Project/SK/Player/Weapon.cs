using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("이펙트 및 사운드")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource audioSource;        // 총 발사 사운드용
    [SerializeField] AudioClip gunShotClip;          // 총 발사 오디오 클립
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();                         // 총구 이펙트
        impulseSource.GenerateImpulse();            // 카메라 흔들림
        audioSource.PlayOneShot(gunShotClip);       // 총 발사 사운드

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
