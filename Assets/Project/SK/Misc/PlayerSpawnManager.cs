using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] string spawnPointName = "BossRoomSpawn";

    void Start()
    {
        GameObject spawnPoint = GameObject.Find(spawnPointName);
        if (spawnPoint)
        {
            transform.position = spawnPoint.transform.position;
            transform.rotation = spawnPoint.transform.rotation;
        }
        else
        {
            Debug.LogWarning("SpawnPoint not found: " + spawnPointName);
        }
    }
}
