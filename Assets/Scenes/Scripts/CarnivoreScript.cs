using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarnivoreScript : MonoBehaviour
{

    AIStates m_State;
    public CharacterController characterController;
    public GameObject ChasingEntity;
    private Vector3 moveDirection, TargetLocation, m_MovementVector;
    private float m_Speed; 

    private float FoodCount;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Phyiscs()
    {
        moveDirection = TargetLocation - this.transform.position;
        m_MovementVector = moveDirection.normalized * m_Speed * 0.05f;
        if (moveDirection.magnitude < 0.5)
        {
            this.transform.position = TargetLocation;
        }
        else
        {
            this.transform.LookAt(TargetLocation);
            characterController.Move(m_MovementVector);
        }
    }
    void StateCheck()
    {
        if (FoodCount > 50)
        {
            m_State = AIStates.Idle;
        }



        StateMachine();
    }


    void StateMachine()
    {
        switch (m_State)
        {
            case AIStates.Idle:
                int rnd = Random.Range(0, 2);
                if (rnd == 0)
                {
                    TargetLocation.x += Random.Range(-10, 11);
                    TargetLocation.z += Random.Range(-10, 11);
                }
                else
                {
                    TargetLocation = transform.position;
                }
                break;
            case AIStates.Fleeing:
                break;
            case AIStates.Finding_Water:
                break;
            case AIStates.Finding_Food:
                break;
            case AIStates.Eating:
                break;
            case AIStates.Drinking:
                break;
        }
    }
}
