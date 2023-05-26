using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpanwer : MonoBehaviour
{
    public static AgentSpanwer instance;

    [Header("Agent settins")]
    [SerializeField] private GameObject agent;
    [Range(3, 5)]
    [SerializeField] private int startAgentsNumber = 3;
    [Range(2, 6)]
    [SerializeField] private float agentSpawnInterval = 3f;
    private float timer = 0f;
    [SerializeField] private int maxAgentsNumber = 30;
    [SerializeField] private int currentAgentsNumber;

    [Space(10)]
    [Header("Spawn points settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnPointsThreshold = 0.5f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnStartingAgents();
    }

    private void Update()
    {
        timer += Time.deltaTime;


        if(currentAgentsNumber == maxAgentsNumber) { return; }
        if (timer >= agentSpawnInterval)
        {
            SpawnAgent();
            timer = 0f;
        }
    }

    public void DecreaseAgentsNumber()
    {
        currentAgentsNumber--;
    }

    private void SpawnAgent()
    {
        Instantiate(agent, GetRandomEmptySpawnPoint().position, Quaternion.identity);
        currentAgentsNumber++;
    }

    private void SpawnStartingAgents()
    {
        while (currentAgentsNumber < startAgentsNumber)
        {
            SpawnAgent();
        }
    }

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

    private Transform GetRandomEmptySpawnPoint()
    {
        Transform emptySpawnPoint = GetEmptySpawnPoints()[Random.Range(0, GetEmptySpawnPoints().Count)];
        return emptySpawnPoint;
    }

    private void OnDrawGizmos()
    {
        foreach(Transform spawnPoint in spawnPoints)
        {
            Gizmos.DrawSphere(spawnPoint.position, spawnPointsThreshold);
        }
    }
}
