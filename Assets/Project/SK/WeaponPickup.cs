using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    const string PLYER_STRING = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLYER_STRING))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            activeWeapon.SwitchWeapon(weaponSO);
            Destroy(this.gameObject);

        }
    }
}
