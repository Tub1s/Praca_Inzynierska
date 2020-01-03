using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

// Created using tutorial https://www.youtube.com/watch?v=blPglabGueM

public class playerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;

    void Start()
    {
        agent.updateRotation = false;
    }
    
    void Update()
    {
        if (agent.remainingDistance > agent.stoppingDistance) character.Move(agent.desiredVelocity, false, false);
        else character.Move(Vector3.zero, false, false);
    }
    
}
