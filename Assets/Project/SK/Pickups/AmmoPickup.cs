using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmount = 100; // ȹ���� ź�� ��
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(ammoAmount); // ActiveWeapon ��ũ��Ʈ�� AdjustAmmo �޼��带 ȣ���Ͽ� ź�� �� ����
    }
}
