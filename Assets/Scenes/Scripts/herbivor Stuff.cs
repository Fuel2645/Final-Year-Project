using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;




public enum AIStates
{
    Idle,
    Fleeing,
    Finding_Food,
    Eating,
    Finding_Water,
    Drinking
}


public class herbivorStuff : MonoBehaviour
{ 
    public CharacterController CharacterController;
    public Vector3 FoodLocaiton, moveDirection;
    public AIStates m_State = AIStates.Idle;
    public float FoodCount = 100;
    private float m_Speed;
    private Vector3 TargetLocation;
    private Vector3 m_MovementVector;
    public bool isMoving = false;
    bool isEating;
    bool isDrinking;

    float ReductionRate;

    public List<GameObject> FoundFood;

    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        m_MovementVector = this.transform.position;
        TargetLocation = this.transform.position;
        //FoodLocaiton = FoodReference.transform.position;
        m_Speed = 10.0f;
        InvokeRepeating("StateCheck", 1.0f, 0.5f);
        InvokeRepeating("Physics", 1.0f, 0.05f);
        InvokeRepeating("FoodDrain", 0, 0.5f);

    }

    // Update is called once per frame

    void StateCheck()
    {
        if (FoodCount > 50)
        {
            m_State = AIStates.Idle;
        }
        else if (FoodCount <= 50 && isMoving == false)
        {
            m_State = AIStates.Finding_Food;
        }
        else if (this.transform.position == TargetLocation && isEating == true)
        {
            m_State = AIStates.Eating;
        }


        Statemachine();
    }

    void FoodDrain()
    {
        ReductionRate = -0.5f;
        //ReductionRate -= m_MovementVector.magnitude;
        FoodCount += ReductionRate;

    }





    void Statemachine()
    {
        switch (m_State)
        {
            case AIStates.Idle:
                int rnd = Random.Range(0, 2);
                if(rnd == 0)
                {
                    TargetLocation.x += Random.Range(-50, 51);
                    TargetLocation.z += Random.Range(-50, 51);
                }
                else
                {
                    TargetLocation = transform.position;
                }
                break;
            case AIStates.Fleeing:
                break;
            case AIStates.Finding_Food:
                if(FoundFood.Count == 0 && isMoving == false)
                {
                    TargetLocation.x += Random.Range(-100, 101);
                    TargetLocation.z += Random.Range(-100, 101);
                    isMoving = true;
                }
                else if(FoodCount == 0 && isMoving == true)
                {
                    if(TargetLocation == this.transform.position)
                    {
                        isMoving = false;
                    }
                }
                else if(isMoving == false)
                {
                    // sorts Foods based on distance from the player in ascending order, closest == first 
                    FoundFood.Sort((x, y) => { return (this.transform.position - x.transform.position).sqrMagnitude.CompareTo((this.transform.position - y.transform.position).sqrMagnitude); });
                    TargetLocation = FoundFood.First().transform.position;
                    isMoving = true;
                }

                break;
            case AIStates.Finding_Water:
                break;
            case AIStates.Drinking:
                break;
            case AIStates.Eating:
                isMoving= false;
                break;
                
        }
    }

    public void Eat()
    {

    }

    void Physics()
    {
        moveDirection = TargetLocation - this.transform.position;
        m_MovementVector = moveDirection.normalized * m_Speed * 0.05f;
        if (moveDirection.magnitude < 0.1)
        {
            this.transform.position = TargetLocation;
        }
        else
        {
            this.transform.LookAt(TargetLocation);
            CharacterController.Move(m_MovementVector);
        }
    }

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            FoundFood.Add(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            FoundFood.Remove(other.gameObject);
        }
        
    }
}
