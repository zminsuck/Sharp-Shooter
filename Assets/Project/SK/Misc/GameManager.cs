using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    [SerializeField] GameObject bossHPUI;
    [SerializeField] GameObject gameClearUI;

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // 현재 씬 이름이 Boss_Sceen일 때만 보스 UI 활성화
        if (SceneManager.GetActiveScene().name == "Boss_Sceen")
        {
            if (bossHPUI != null)
                bossHPUI.SetActive(true);
        }
    }

    public void OnBossDefeated()
    {
        // 게임 클리어 UI 활성화
        if (gameClearUI != null)
            gameClearUI.SetActive(true);

        // (선택 사항) 게임 일시정지
        // Time.timeScale = 0f;
    }

    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
