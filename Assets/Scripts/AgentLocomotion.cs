using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentHealth))]
public class AgentLocomotion : MonoBehaviour
{
    [SerializeField] private float agentSpeed;
    [SerializeField] private float floorLenght;
    private AgentHealth agentHealth;

    void Start()
    {
        agentHealth = GetComponent<AgentHealth>();        
    }

    void Update()
    {

        transform.Translate(Vector3.forward * agentSpeed * Time.deltaTime);

        if(Mathf.Abs(transform.position.x) >= floorLenght || Mathf.Abs(transform.position.z) >= floorLenght)
        {
            TurnBack();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeDirection();
        }
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
