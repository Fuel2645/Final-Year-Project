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
    public CharacterController characterController;
    public Vector3 FoodLocaiton, moveDirection;
    public AIStates m_State = AIStates.Idle;
    public float FoodCount = 100;
    private float m_Speed;
    private Vector3 TargetLocation;
    private Vector3 m_MovementVector;
    public bool isMoving = false;
    public bool isEating;
    bool isDrinking;
    float ReductionRate;
    private GameObject chasingCarnivore;
    public List<GameObject> FoundFood;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
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
        if(FoundFood.Count > 1)
        {
            FoundFood.RemoveAll(item => item == null);
        }
        else if(FoundFood.Count > 0)
        {
            if(FoundFood.FirstOrDefault() == null)
            {
                FoundFood.Remove(FoundFood.FirstOrDefault());   
            }
        }
        
        if (m_State != AIStates.Fleeing)
        {   
            if (FoodCount > 50)
            {
                m_State = AIStates.Idle;
            }
            else if (FoodCount <= 50 && isMoving == false)
            {
                print("Help");
                m_State = AIStates.Finding_Food;
            }
            else if (this.transform.position == TargetLocation && isEating == true)
            {
                m_State = AIStates.Eating;
            }
            Statemachine();
        }
        else
        {
            if (chasingCarnivore != null && Vector3.Distance(chasingCarnivore.transform.position, this.transform.position) > 50)
            {
                m_State = AIStates.Idle;
            }
            else if (chasingCarnivore != null && this.transform.position == TargetLocation)
            {
                TargetLocation += (this.transform.position - chasingCarnivore.transform.position).normalized * 10;
                print("LocationInfo");
            }
        }
    }

    void FoodDrain()
    {
        ReductionRate = -2.5f;
        //ReductionRate -= m_MovementVector.magnitude;
        FoodCount += ReductionRate;

    }

    void EatFood(GameObject foodRef)
    {
        Destroy(foodRef);
    }



    void Statemachine()
    {
        switch (m_State)
        {
            case AIStates.Idle:
                int rnd = Random.Range(0, 2);
                if(rnd == 0)
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
            case AIStates.Finding_Food:
                if(FoundFood.Count == 0)
                {
                    TargetLocation.x += Random.Range(-10, 10);
                    TargetLocation.z += Random.Range(-10, 10);
                }
                if(isMoving == true)
                {
                    if(TargetLocation == this.transform.position)
                    {
                        isEating = true;
                        isMoving = false;
                    }
                }
                if(isMoving == false)
                {
                    // sorts Foods based on distance from the player in ascending order, closest == first 
                    if(FoundFood.Count > 1)
                    {
                        print("Sorting");
                        FoundFood.Sort((x, y) => { return (this.transform.position - x.transform.position).sqrMagnitude.CompareTo((this.transform.position - y.transform.position).sqrMagnitude); });
                        TargetLocation = FoundFood.FirstOrDefault().transform.position;
                        isMoving = true;
                    }
                    else if(FoundFood.Count > 0)
                    {
                        TargetLocation = FoundFood.FirstOrDefault().transform.position;
                        isMoving = true;
                    }
                }

                break;
            case AIStates.Finding_Water:
                break;
            case AIStates.Drinking:
                break;
            case AIStates.Eating:
                isEating = false;
                isMoving = false;
                FoodCount += FoundFood.First().GetComponent<FoodData>().FoodValue;
                GameObject gameObject = FoundFood.FirstOrDefault();
                FoundFood.Remove(FoundFood.FirstOrDefault());
                EatFood(gameObject);

                break;
                
        }
    }

    void SightClear()
    {
        for (int i = FoundFood.Count; i < 0; i--)
        {
            if (FoundFood[i] == null)
            {
                FoundFood.RemoveAt(i);
            }
        }
    }

    public void Eat()
    {

    }

    void Physics()
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

    
    public void addEntitiy(GameObject other)
    {
            if (other.gameObject.tag == ("Food") && !FoundFood.Contains(other.gameObject))
            {
                print("balls");
                if (FoundFood.Count == 0)
                {
                    TargetLocation = other.transform.position;
                }


                FoundFood.Add(other.gameObject);

            }
            else if(other.gameObject.tag == ("Carnivore"))
            {
                print("Carnivore");
                m_State = AIStates.Fleeing;
                TargetLocation += (this.transform.position - other.transform.position).normalized * 10;
                chasingCarnivore = other.gameObject;
            }
            else print("dick");

    }

    public void removeEntity(GameObject other)
    {
        if (other.gameObject.tag == ("Food") && FoundFood.Contains(other.gameObject))
        {
            FoundFood.Remove(other.gameObject);
        }
    }
}
