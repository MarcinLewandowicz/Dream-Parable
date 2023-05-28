using TMPro;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(AgentHealth))]
public class AgentUI : MonoBehaviour
{
    private AgentHealth agentHealth;
    [SerializeField] private RawImage healthBarImage;
    [SerializeField] private TextMeshProUGUI agentName;
    private int startHealthPoints;

    private void Awake()
    {
        agentHealth = GetComponent<AgentHealth>();
        startHealthPoints = agentHealth.AgentHealthPoints;
    }

    private void Start()
    {
        agentName.text = gameObject.name;
    }

    private void OnEnable()
    {
        agentHealth.OnDamageTaken += UpdateHealthPoints;
    }

    private void OnDisable()
    {
        agentHealth.OnDamageTaken -= UpdateHealthPoints;
    }

    private void UpdateHealthPoints()
    {
        healthBarImage.rectTransform.localScale = new Vector3((float)agentHealth.AgentHealthPoints / startHealthPoints, 1, 1);
    }
}
