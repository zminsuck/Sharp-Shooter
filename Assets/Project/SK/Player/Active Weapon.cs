using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    // 플레이어 시점을 따라다니는 가상 카메라
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    // 줌 상태에서 화면에 띄울 UI(흔히 vignette 효과)
    [SerializeField] GameObject zoomVignette;
    [SerializeField] TMP_Text ammoText; // 현재 탄약 수를 표시할 UI 텍스트

    WeaponSO currentWeaponSO;
    Animator animator;  // 애니메이터 컴포넌트 (사격 애니메이션 재생용)
    StarterAssetsInputs starterAssetsInputs; // 입력 값 처리용 (마우스 클릭, 줌 등)
    FirstPersonController firstPersonController; // 1인칭 컨트롤러 (줌 시 회전 속도 변경용)
    Weapon currentWeapon; // 실제 발사 로직이 있는 Weapon 스크립트

    // 애니메이터에서 사용할 트리거 이름
    const string SHOOT_STRING = "Shoot";
    // 마지막 샷 이후 누적된 시간
    float timeSinceLastShot = 0f;
    // 카메라의 기본 시야각(FOV) 저장
    float defaultFOV;
    // 컨트롤러의 기본 회전 속도 저장
    float defaultRotationSpeed;
    int currentAmmo;

    void Awake()
    {
        // 부모 객체(플레이어)에서 입력 및 컨트롤러 컴포넌트 가져오기
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        // 같은 객체에 붙은 애니메이터 가져오기
        animator = GetComponent<Animator>();
        // 초기 카메라 FOV와 회전 속도 저장
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
        // 매 프레임마다 발사 및 줌 처리
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

    /// 외부에서 무기를 교체할 때 호출

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        // 기존 무기가 있으면 제거
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }
        // 새로운 무기 프리팹을 생성하고 Weapon 컴포넌트 할당
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        // 데이터도 업데이트
        this.currentWeaponSO = weaponSO;
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }


    /// 사격 입력 처리
    void HandleShoot()
    {
        // 누적 시간 증가
        timeSinceLastShot += Time.deltaTime;

        // 발사 버튼이 눌리지 않았다면 리턴
        if (!starterAssetsInputs.shoot) return;

        // 발사 간격이 지난 경우에만 발사
        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            // 실제 발사 로직 실행
            currentWeapon.Shoot(currentWeaponSO);
            // 애니메이션 트리거 재생
            animator.Play(SHOOT_STRING, 0, 0f);
            // 누적 시간 초기화
            timeSinceLastShot = 0f;
            AdjustAmmo(-1); // 현재 탄약 수 감소
        }

        // 자동 사격이 아닌 무기면, 발사 입력을 초기화
        if (!currentWeaponSO.isAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    /// 줌(조준) 입력 처리

    void HandleZoom()
    {
        // 줌 기능이 없는 무기면 리턴
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            // 줌 시작: FOV 축소, vignette 켜기, 회전 속도 변경
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            // 줌 해제: 기본 FOV 복원, vignette 끄기, 회전 속도 복원
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
