using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmount = 100; // 획득할 탄약 수
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(ammoAmount); // ActiveWeapon 스크립트의 AdjustAmmo 메서드를 호출하여 탄약 수 조정
    }
}
