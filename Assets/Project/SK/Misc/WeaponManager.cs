using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponSO[] weaponSlots;
    [SerializeField] ActiveWeapon activeWeapon;

    int currentWeaponIndex = -1;

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame) EquipWeapon(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) EquipWeapon(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) EquipWeapon(2);
    }

    void EquipWeapon(int index)
    {
        if (index < 0 || index >= weaponSlots.Length) return;
        if (index == currentWeaponIndex) return;

        currentWeaponIndex = index;
        activeWeapon.SwitchWeapon(weaponSlots[index]);
        Debug.Log($"무기 {index + 1} 교체 완료: {weaponSlots[index].weaponPrefab.name}");
    }
}
