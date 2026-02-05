using UnityEngine;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour
{
    [Header("NPC Settings")]
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private int npcCount = 50;
    [SerializeField] private float spawnRadius = 50f;

    [Header("Spawn Area")]
    [SerializeField] private Vector3 spawnCenter = Vector3.zero;
    [SerializeField] private bool useTerrainHeight = true;
    [SerializeField] private LayerMask groundLayer = -1; // Default to everything
    [SerializeField] private float raycastHeight = 500f;

    [Header("Advanced")]
    [SerializeField] private bool spawnOnStart = true;
    [SerializeField] private float minDistanceBetweenNPCs = 2f;

    private List<GameObject> spawnedNPCs = new List<GameObject>();

    void Start()
    {
        if (spawnOnStart)
        {
            SpawnNPCs();
        }
    }

    public void SpawnNPCs()
    {
        ClearNPCs();

        if (npcPrefab == null)
        {
            Debug.LogError("NPC Prefab is not assigned!");
            return;
        }

        for (int i = 0; i < npcCount; i++)
        {
            Vector3 spawnPos = GetValidSpawnPosition();

            if (spawnPos != Vector3.zero)
            {
                GameObject npc = Instantiate(npcPrefab, spawnPos, Quaternion.identity);
                npc.name = $"NPC_{i}";
                npc.transform.parent = transform;

                // Ensure NPC is upright
                npc.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

                spawnedNPCs.Add(npc);
            }
        }

        Debug.Log($"Spawned {spawnedNPCs.Count} NPCs");
    }

    private Vector3 GetValidSpawnPosition()
    {
        int maxAttempts = 30;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = spawnCenter + new Vector3(randomCircle.x, raycastHeight, randomCircle.y);

            if (useTerrainHeight)
            {
                RaycastHit hit;
                // Cast ray downward from high position
                if (Physics.Raycast(spawnPos, Vector3.down, out hit, raycastHeight * 2f, groundLayer))
                {
                    spawnPos = hit.point + Vector3.up * 0.1f; // Slight offset above ground
                    Debug.DrawRay(spawnPos, Vector3.up * 2f, Color.green, 2f);
                }
                else
                {
                    Debug.DrawRay(spawnPos, Vector3.down * 10f, Color.red, 2f);
                    continue; // No ground found, try again
                }
            }

            // Check if position is far enough from other NPCs
            if (IsPositionValid(spawnPos))
            {
                return spawnPos;
            }
        }

        Debug.LogWarning("Could not find valid spawn position after max attempts");
        return Vector3.zero;
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (GameObject npc in spawnedNPCs)
        {
            if (npc != null && Vector3.Distance(position, npc.transform.position) < minDistanceBetweenNPCs)
            {
                return false;
            }
        }
        return true;
    }

    public void ClearNPCs()
    {
        foreach (GameObject npc in spawnedNPCs)
        {
            if (npc != null)
            {
                Destroy(npc);
            }
        }
        spawnedNPCs.Clear();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnCenter, spawnRadius);
    }
}