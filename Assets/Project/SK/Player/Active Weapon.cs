using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    // �÷��̾� ������ ����ٴϴ� ���� ī�޶�
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    // �� ���¿��� ȭ�鿡 ��� UI(���� vignette ȿ��)
    [SerializeField] GameObject zoomVignette;
    [SerializeField] TMP_Text ammoText; // ���� ź�� ���� ǥ���� UI �ؽ�Ʈ

    WeaponSO currentWeaponSO;
    Animator animator;  // �ִϸ����� ������Ʈ (��� �ִϸ��̼� �����)
    StarterAssetsInputs starterAssetsInputs; // �Է� �� ó���� (���콺 Ŭ��, �� ��)
    FirstPersonController firstPersonController; // 1��Ī ��Ʈ�ѷ� (�� �� ȸ�� �ӵ� �����)
    Weapon currentWeapon; // ���� �߻� ������ �ִ� Weapon ��ũ��Ʈ

    // �ִϸ����Ϳ��� ����� Ʈ���� �̸�
    const string SHOOT_STRING = "Shoot";
    // ������ �� ���� ������ �ð�
    float timeSinceLastShot = 0f;
    // ī�޶��� �⺻ �þ߰�(FOV) ����
    float defaultFOV;
    // ��Ʈ�ѷ��� �⺻ ȸ�� �ӵ� ����
    float defaultRotationSpeed;
    int currentAmmo;

    void Awake()
    {
        // �θ� ��ü(�÷��̾�)���� �Է� �� ��Ʈ�ѷ� ������Ʈ ��������
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        // ���� ��ü�� ���� �ִϸ����� ��������
        animator = GetComponent<Animator>();
        // �ʱ� ī�޶� FOV�� ȸ�� �ӵ� ����
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    void Update()
    {
        // �� �����Ӹ��� �߻� �� �� ó��
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        ammoText.text = currentAmmo.ToString("D2");
    }

    /// �ܺο��� ���⸦ ��ü�� �� ȣ��

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        // ���� ���Ⱑ ������ ����
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }
        // ���ο� ���� �������� �����ϰ� Weapon ������Ʈ �Ҵ�
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        // �����͵� ������Ʈ
        this.currentWeaponSO = weaponSO;
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }


    /// ��� �Է� ó��
    void HandleShoot()
    {
        // ���� �ð� ����
        timeSinceLastShot += Time.deltaTime;

        // �߻� ��ư�� ������ �ʾҴٸ� ����
        if (!starterAssetsInputs.shoot) return;

        // �߻� ������ ���� ��쿡�� �߻�
        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            // ���� �߻� ���� ����
            currentWeapon.Shoot(currentWeaponSO);
            // �ִϸ��̼� Ʈ���� ���
            animator.Play(SHOOT_STRING, 0, 0f);
            // ���� �ð� �ʱ�ȭ
            timeSinceLastShot = 0f;
            AdjustAmmo(-1); // ���� ź�� �� ����
        }

        // �ڵ� ����� �ƴ� �����, �߻� �Է��� �ʱ�ȭ
        if (!currentWeaponSO.isAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    /// ��(����) �Է� ó��

    void HandleZoom()
    {
        // �� ����� ���� ����� ����
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            // �� ����: FOV ���, vignette �ѱ�, ȸ�� �ӵ� ����
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            // �� ����: �⺻ FOV ����, vignette ����, ȸ�� �ӵ� ����
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
