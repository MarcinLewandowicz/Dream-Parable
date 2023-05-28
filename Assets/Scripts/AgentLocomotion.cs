using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentHealth))]
public class AgentLocomotion : MonoBehaviour
{
    [SerializeField] private float agentSpeed;
    [SerializeField] private float floorXLength;
    [SerializeField] private float floorZLength;

    private AgentHealth agentHealth;

    void Start()
    {
        agentHealth = GetComponent<AgentHealth>();        
    }

    void Update()
    {

        transform.Translate(Vector3.forward * agentSpeed * Time.deltaTime);

        if(Mathf.Abs(transform.position.x) >= floorXLength/2 || Mathf.Abs(transform.position.z) >= floorZLength/2)
        {
            TurnBack();
        }
    }

    public void SetAgentSpeed(float speed)
    {
        agentSpeed = speed;
    }

    public void SetFloorSize(float floorXLength, float floorZLength)
    {
        this.floorXLength = floorXLength;
        this.floorZLength = floorZLength;
    }

    private void ChangeDirection()
    {
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    private void TurnBack()
    {
        transform.Rotate(0, transform.rotation.y - 180, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Agent")
        {
            agentHealth.TakeDamage(1);
            ChangeDirection();
        }
    }
}
