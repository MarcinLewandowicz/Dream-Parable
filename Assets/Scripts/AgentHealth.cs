using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{

    public UnityAction OnDamageTaken;
    [SerializeField] private int agentHealthPoints = 3;
    public int AgentHealthPoints { get { return agentHealthPoints; } }

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
        OnDamageTaken?.Invoke();
    }

    private void DeathBehaviour()
    {
        AgentSpanwer.instance.OnAgentsDeath(gameObject);
        Destroy(gameObject);
    }
}
