using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] Transform spawnPoint;

    PlayerHealth player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(spawnTime);

        // �� �������� ������Ʈ�� �ı��Ǿ����� �ٽ� üũ
        if (!this || !gameObject.activeInHierarchy) yield break;

        Instantiate(enemyPrefab, spawnPoint.position, transform.rotation);
    }
}
