using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpanwer : MonoBehaviour
{
    [Header("Agent settins")]
    [SerializeField] private GameObject agent;
    [Range(3, 5)]
    [SerializeField] private int startAgentsNumber = 3;
    [Range(3, 6)]
    [SerializeField] private float agentsSpawnInterval = 3f;
    [SerializeField] private int maxAgentsNumber = 30;
    public int currentAgentsNumber;

    [Space(10)]
    [Header("Spawn points settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnPointsThreshold = 0.5f;


    private List<Transform> GetEmptySpawnPoints()
    {
        List<Transform> emptySpawnPoints = new List<Transform>();
        foreach (Transform spawnPoint in spawnPoints)
        {
            Collider[] hitColliders = Physics.OverlapSphere(spawnPoint.position, spawnPointsThreshold);
            if (hitColliders.Length == 0)
            {
                emptySpawnPoints.Add(spawnPoint);
            }
        }
        return emptySpawnPoints;
    }

    private void OnDrawGizmos()
    {
        foreach(Transform spawnPoint in spawnPoints)
        {
            Gizmos.DrawSphere(spawnPoint.position, spawnPointsThreshold);
        }
    }
}
