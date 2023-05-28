using UnityEngine;

public class AgentSelector : MonoBehaviour
{

    [SerializeField] private GameObject agentUI;

    public void SetUIState(bool state)
    {
        agentUI.SetActive(state);
    }

    private void OnMouseDown()
    {
        AgentSpanwer.instance.agentSelectManager.SelectAgent(gameObject);
    }
}
