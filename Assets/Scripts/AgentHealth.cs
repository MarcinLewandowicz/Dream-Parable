using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{

    [SerializeField] private int agentHealthPoints = 3;

    public void TakeDamage(int damage)
    {
        if(agentHealthPoints - damage > 0)
        {
            agentHealthPoints -= damage;
        }
        else
        {
            DeathBehaviour();
        }
    }

    private void DeathBehaviour()
    {
        AgentSpanwer.instance.OnAgentsDeath(gameObject);
        Destroy(gameObject);
    }
}
