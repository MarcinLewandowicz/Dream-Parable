using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSelectManager : MonoBehaviour
{
    public List<GameObject> AgentsToSelect;
    private GameObject selectedAgent;

    public void AddAgentToSelect(GameObject agent)
    {
        AgentsToSelect.Add(agent);
    }

    public void RemoveAgentToSelect(GameObject agent)
    {
        AgentsToSelect.Remove(agent);
    }

    public void SelectAgent(GameObject agentSelected)
    {
        if (selectedAgent == agentSelected)
        {
            selectedAgent = null;
        }
        else
        {
            selectedAgent = agentSelected.gameObject;
        }
        foreach (GameObject agent in AgentsToSelect)
        {
            agent.GetComponent<AgentSelector>().SetUIState(agent == selectedAgent);
        }
    }
}
