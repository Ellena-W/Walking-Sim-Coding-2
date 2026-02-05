using UnityEngine;

public class PenguinSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int penguinCount = 10;
    public float spawnRadius = 20f;

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < penguinCount; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(-84, 0, -104);

            Instantiate(npcPrefab, spawnPos, Quaternion.identity);
        }
    }
}