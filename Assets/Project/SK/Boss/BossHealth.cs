using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween 사용을 위한 네임스페이스

public class BossHealth : MonoBehaviour
{
    [Header("보스 데이터")]
    [SerializeField] EnemyDataSO bossData;

    [Header("UI")]
    [SerializeField] Image hpBarFill;              // 체력바 이미지 (Image Type = Filled)
    [SerializeField] Color normalColor = Color.red;
    [SerializeField] Color flashColor = Color.white;
    [SerializeField] float flashDuration = 0.1f;
    [SerializeField] float animationDuration = 0.5f;
    [SerializeField] Ease animationEase = Ease.OutQuad;

    int currentHealth;
    bool isDead = false;

    private void Start()
    {
        currentHealth = bossData.maxHealth;

        if (hpBarFill != null)
        {
            hpBarFill.fillAmount = 1f;
            hpBarFill.color = normalColor;
        }

        GameObject bossHPUI = GameObject.Find("BossHPUI");
        if (bossHPUI != null)
        {
            bossHPUI.SetActive(true);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        float targetRatio = (float)currentHealth / bossData.maxHealth;

        AnimateHealthBar(targetRatio);
        StartCoroutine(FlashEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void AnimateHealthBar(float targetRatio)
    {
        if (hpBarFill != null)
        {
            hpBarFill.DOFillAmount(targetRatio, animationDuration)
                     .SetEase(animationEase);
        }
    }

    System.Collections.IEnumerator FlashEffect()
    {
        if (hpBarFill != null)
        {
            hpBarFill.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            hpBarFill.color = normalColor;
        }
    }

    void Die()
    {
        isDead = true;

        if (bossData.deathVFX)
        {
            Instantiate(bossData.deathVFX, transform.position, Quaternion.identity);
        }

        // 게임 매니저에 보스 사망 알림
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnBossDefeated();
        }

        Destroy(gameObject);
    }

}
