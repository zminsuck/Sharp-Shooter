using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween ����� ���� ���ӽ����̽�

public class BossHealth : MonoBehaviour
{
    [Header("���� ������")]
    [SerializeField] EnemyDataSO bossData;

    [Header("UI")]
    [SerializeField] Image hpBarFill;              // ü�¹� �̹��� (Image Type = Filled)
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

        // ���� �Ŵ����� ���� ��� �˸�
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnBossDefeated();
        }

        Destroy(gameObject);
    }

}
