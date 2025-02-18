using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarnivoreScript : MonoBehaviour
{

    public AIStates m_State;
    public CharacterController characterController;
    public GameObject ChasingEntity;
    public Vector3 moveDirection, TargetLocation, m_MovementVector;
    private float m_Speed;
    private float HuntChance;

    private float FoodCount = 100;
    public float DesireToHunt;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        m_MovementVector = this.transform.position;
        TargetLocation = this.transform.position;
        m_Speed = 10.0f;

        InvokeRepeating("StateCheck", 1.0f, 0.5f);
        InvokeRepeating("Phyiscs", 0.0f, 0.05f);
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
            //print("Stuck");
        }
        else
        {
            //print("Moving");

            this.transform.LookAt(TargetLocation);
            characterController.Move(m_MovementVector);
        }
    }
    void StateCheck()
    {
        HuntChance = 100 - FoodCount - DesireToHunt;  

        if (HuntChance > 50)
        {
            m_State = AIStates.Idle;
        }
        else if (HuntChance  < 50)
        {
            m_State = AIStates.Finding_Food;
        }



        StateMachine();
    }

    public void SpottedEntity(GameObject Entity)
    {
        if (Entity.tag == ("Herbivore"))
        {
            if (ChasingEntity == null)
            {
                ChasingEntity = Entity;
            }
            else
            {
                if (Vector3.Distance(this.transform.position, Entity.transform.position) < Vector3.Distance(this.transform.position, ChasingEntity.transform.position))
                {
                    ChasingEntity = Entity;
                }
            }
        }
    }

    public void ForgetEntity(GameObject Entity)
    {
        if(ChasingEntity == Entity)
        {
            ChasingEntity = null;
        }
    }


    void StateMachine()
    {
        switch (m_State)
        {
            case AIStates.Idle:
                int rnd = Random.Range(0, 2);
                print("Carnviore Move");
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
