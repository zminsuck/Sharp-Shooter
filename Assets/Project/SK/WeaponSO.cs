using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab; // ���� ������
    public int Damage = 1; // ���� ������
    public float FireRate = .5f; // ���� �߻� �ӵ�
    public GameObject HitVFXPrefab; // �� ��Ʈ ���� ����Ʈ
    public bool isAutomatic = false; // �ڵ� �߻� ����
    public bool CanZoom = false; // �� ���� ����
    public float ZoomAmount = 10f;
    public float ZoomRotationSpeed = .3f;
}
