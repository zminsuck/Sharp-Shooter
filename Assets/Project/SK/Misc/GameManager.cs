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
        // �̱��� ����
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // ���� �� �̸��� Boss_Sceen�� ���� ���� UI Ȱ��ȭ
        if (SceneManager.GetActiveScene().name == "Boss_Sceen")
        {
            if (bossHPUI != null)
                bossHPUI.SetActive(true);
        }
    }

    public void OnBossDefeated()
    {
        // ���� Ŭ���� UI Ȱ��ȭ
        if (gameClearUI != null)
            gameClearUI.SetActive(true);

        // (���� ����) ���� �Ͻ�����
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
