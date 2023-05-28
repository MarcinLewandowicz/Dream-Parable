using System.Collections.Generic;
using UnityEngine;

public class AgentSpanwer : MonoBehaviour
{
    public static AgentSpanwer instance;

    [Header("Agent settings")]
    [SerializeField] private GameObject agent;
    [SerializeField] private float agentSpeed;
    [Range(3, 5)]
    [SerializeField] private int startAgentsNumber = 3;
    private float timer = 0f;
    [Range(0,30)]
    [SerializeField] private int maxAgentsNumber = 30;
    [Header("Spawn interval settings")]
    [Range(2,6)]
    [SerializeField] private float agentSpawnMinInterval = 2f;
    [Range(2, 6)]
    [SerializeField] private float agentSpawnMaxInterval = 3f;
    private float currentSpawnInterval;
    private int currentAgentsNumber;
    public AgentSelectManager agentSelectManager { get; private set; }
    [Header("Agent name settings")]
    [Range(3, 6)]
    [SerializeField] private int agentNameMinLength = 3;
    [Range(6, 10)]
    [SerializeField] private int agentNameMaxLength = 8;
    private NameGenerator nameGenerator = new();

    [Space(10)]
    [Header("Spawn points settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnPointsThreshold = 0.5f;
    [SerializeField] private LayerMask playerLayerMask;

    [Space(10)]
    [Header("Map settings")]
    [Min(10)]
    [SerializeField] private float mapXLength;
    [Min(10)]
    [SerializeField] private float mapZLength;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        agentSelectManager = GetComponent<AgentSelectManager>();
    }

    private void Start()
    {
        SpawnStartingAgents();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (currentAgentsNumber == maxAgentsNumber) { return; }
        if (timer >= currentSpawnInterval)
        {
            SpawnAgent();
            timer = 0f;
        }
    }

    public void OnAgentsDeath(GameObject agent)
    {
        currentAgentsNumber--;
        agentSelectManager.RemoveAgentToSelect(agent);
    }

    private void SpawnAgent()
    {
        if (GetRandomEmptySpawnPoint() == null) { return; }
        GameObject spawnedAgent = Instantiate(agent, GetRandomEmptySpawnPoint().position, GetRandomEmptySpawnPoint().rotation);
        spawnedAgent.name = nameGenerator.GetRandomName(Random.Range(agentNameMinLength, agentNameMaxLength));
        AgentLocomotion agentLocomotion = agent.GetComponent<AgentLocomotion>();
        agentLocomotion.SetAgentSpeed(agentSpeed);
        agentLocomotion.SetFloorSize(mapXLength, mapZLength);
        currentAgentsNumber++;
        agentSelectManager.AddAgentToSelect(spawnedAgent);
        currentSpawnInterval = Random.Range(agentSpawnMinInterval, agentSpawnMaxInterval);
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
            bool spawnPointOccupied = Physics.CheckSphere(spawnPoint.position, spawnPointsThreshold, playerLayerMask);
            if (!spawnPointOccupied)
            {
                emptySpawnPoints.Add(spawnPoint);
            }
        }
        if(emptySpawnPoints.Count == 0)
        {
            return null;
        }
        return emptySpawnPoints;
    }

    private Transform GetRandomEmptySpawnPoint()
    {
        if (GetEmptySpawnPoints() != null)
        {
            Transform emptySpawnPoint = GetEmptySpawnPoints()[Random.Range(0, GetEmptySpawnPoints().Count)];
            return emptySpawnPoint;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Gizmos.DrawSphere(spawnPoint.position, spawnPointsThreshold);
        }
    }
}
