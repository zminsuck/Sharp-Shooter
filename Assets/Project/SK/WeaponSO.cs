using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab; // 무기 프리팹
    public int Damage = 1; // 공용 데미지
    public float FireRate = .5f; // 공용 발사 속도
    public GameObject HitVFXPrefab; // 적 히트 생성 이펙트
    public bool isAutomatic = false; // 자동 발사 여부
    public bool CanZoom = false; // 줌 가능 여부
    public float ZoomAmount = 10f;
    public float ZoomRotationSpeed = .3f;
}
