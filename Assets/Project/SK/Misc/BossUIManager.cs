using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    [Header("UI 구성 요소")]
    [SerializeField] GameObject bossUIContainer;
    [SerializeField] Slider healthSlider;
    [SerializeField] Text bossNameText;

    public void SetupBossUI(string bossName, int maxHealth)
    {
        bossUIContainer.SetActive(true);
        bossNameText.text = bossName;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void UpdateHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    public void HideUI()
    {
        bossUIContainer.SetActive(false);
    }
}
