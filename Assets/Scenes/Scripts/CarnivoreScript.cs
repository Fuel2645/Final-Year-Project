using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class CarnivoreScript : MonoBehaviour
{

    public AIStates m_State;
    

    private float m_Speed;
    private float HuntChance;
    public float FoodCount = 100;
    public float WaterCount = 100;
    private float DesireToHunt;
    private float ReductionRate = 2.5f;
    private float BoundX1, BoundX2, BoundZ1, BoundZ2;
    private float Distancce;
    private bool isMoving = false;
    private int NeedToHunt;
    public int m_Health;
    private Vector3 moveDirection, TargetLocation, m_MovementVector, ReachBox;
    public List<GameObject> FoundWater;
    private GameObject CorspeRef;
    private CharacterController characterController;
    public GameObject ChasingEntity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        m_MovementVector = this.transform.position;
        TargetLocation = this.transform.position;
        ReachBox = new Vector3(1.5f, 5.0f, 1.5f);
    }



    // Update is called once per frame
    void Update()
    {
    }

    public void initialise(float BoundX, float BoundZ, int Health, float Speed, GameObject Corpse)
    {
        CorspeRef = Corpse;
        float NextBirthingTime = Random.Range(60.0f, 121.0f);

        print("Carnivore Start");

        m_Speed = Speed;
        m_State = AIStates.Idle;
        m_Health = Health;
        NeedToHunt = UnityEngine.Random.Range(50, 71);

        BoundX1 = BoundX;
        BoundX2 = -1 * BoundX;
        BoundZ1 = BoundZ;
        BoundZ2 = -1 * BoundZ;

        float rnd1, rnd2;
        rnd1 = Random.Range(0.2f, 0.9f);
        rnd2 = Random.Range(0.7f, 1.3f);

        InvokeRepeating("StateCheck", rnd2, rnd1);
        InvokeRepeating("Phyiscs", 0.0f, 0.05f);
        InvokeRepeating("FoodDrain", 1.0f, 0.5f);
        InvokeRepeating("WaterDrain", 1.0f, 1.0f);
        Invoke("Birthing", NextBirthingTime);
    }


    void Phyiscs()
    {
        if (TargetLocation.x >= BoundX1)
        {
            TargetLocation.x = BoundX1;
        }
        else if (TargetLocation.x <= BoundX2)
        {
            TargetLocation.x = BoundX2;
        }

        if (TargetLocation.z >= BoundZ1)
        {
            TargetLocation.z = BoundZ1;
        }
        else if (TargetLocation.z <= BoundZ2)
        {
            TargetLocation.z = BoundZ2;
        }


        moveDirection = TargetLocation - this.transform.position;
        m_MovementVector = moveDirection.normalized * m_Speed * 0.05f;
        if (Vector3.Distance(this.transform.position, TargetLocation) <= Vector3.Distance(this.transform.position + m_MovementVector, TargetLocation))
        {
            this.transform.position = TargetLocation;
            isMoving = false;
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
        HuntChance = 0 + FoodCount - DesireToHunt;  
        //

        if(m_State == AIStates.Idle)
        {
            if (HuntChance > NeedToHunt)
            {
                m_State = AIStates.Idle;
            }
            else if (WaterCount <= 60)
            {
                m_State = AIStates.Finding_Water;
            }
            else if (HuntChance <= NeedToHunt)
            {
                m_State = AIStates.Finding_Food;
            } 
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
                    TargetLocation = this.transform.position;
                    TargetLocation.y = 0;
                    TargetLocation.x += Random.Range(-10, 11);
                    TargetLocation.z += Random.Range(-10, 11);
                }
                else
                {
                    TargetLocation = this.transform.position;
                    
                }
                break;
            case AIStates.Fleeing:
                break;
            case AIStates.Finding_Water:
                if(FoundWater.Count ==0)
                {
                    TargetLocation = this.transform.position;
                    TargetLocation.y = 0;
                    TargetLocation.x += Random.Range(-10, 11);
                    TargetLocation.z += Random.Range(-10, 11);
                }
                else if(isMoving == true)
                {
                    foreach (Collider collider in Physics.OverlapBox(this.transform.position, ReachBox))
                    {
                        if(collider.tag == "Water")
                        {
                            m_State = AIStates.Drinking;
                            isMoving = false;
                        }
                        
                    }
                }
                else if(isMoving == false)
                {
                    if(FoundWater.Count > 1)
                    {
                        FoundWater.Sort((x, y) => { return (this.transform.position - x.transform.position).sqrMagnitude.CompareTo((this.transform.position - y.transform.position).sqrMagnitude); });
                        TargetLocation = FoundWater.FirstOrDefault().transform.position;
                        isMoving = true;

                    }
                    else if(FoundWater.Count > 0)
                    {
                        TargetLocation = FoundWater.FirstOrDefault().transform.position;
                        isMoving = true;
                    }
                }
                break;
            case AIStates.Finding_Food:
                if(ChasingEntity == null)
                {
                    if(!isMoving)
                    {
                        TargetLocation = this.transform.position;
                        TargetLocation.x += Random.Range(-20, 21);
                        TargetLocation.z += Random.Range(-20, 21);
                        isMoving = true;
                    }
                    else
                    {
                        if(this.transform.position == TargetLocation)
                        {
                            isMoving = false;
                        }
                    }
                    
                }
                
                else if (ChasingEntity != null)
                {
                    if (Vector3.Distance(this.transform.position, ChasingEntity.transform.position) <= 7)
                    {
                        print("Eatrings");
                        m_State = AIStates.Eating;
                        isMoving = false;
                    }
                    else
                    {
                        TargetLocation = ChasingEntity.transform.position;
                        isMoving = true;
                    }
                }
                
                break;
            case AIStates.Eating:
                if(ChasingEntity == null)
                {
                    m_State = AIStates.Finding_Food;
                    break;
                }
                else if (ChasingEntity.tag == "Corpse" && ChasingEntity != null)
                {
                    print("Eating Corpse");
                    ChasingEntity.GetComponent<CorspseScript>().EatMe();
                }
                else if (ChasingEntity.tag == "Herbivore" && ChasingEntity != null)
                {
                    print("Tesitng");
                    ChasingEntity.GetComponent<herbivorStuff>().GetEatenBitch();
                }

                FoodCount += 50;
                print("Poop " + FoodCount + " " + this.gameObject.name); 
                NeedToHunt = Random.Range(50, 71);
                m_State = AIStates.Idle;
                ChasingEntity = null;
                break;
            case AIStates.Drinking:
                FoundWater.First().GetComponent<WaterScript>().DrinkingWater();
                WaterCount += 40;
                m_State = AIStates.Idle;
                break;
        }
    }

    private void Birthing()
    {
        if(m_Health >= 50 && FoodCount >= 50 && WaterCount >= 50)
        {
            print("Giving Birth");
            GameObject child = new GameObject();
            float childSpeed = Random.Range(m_Speed - 0.2f, m_Speed + 0.3f);
            child = Instantiate(this.gameObject, this.transform.position, this.transform.rotation);
            child.GetComponent<CarnivoreScript>().initialise(BoundX1, BoundZ1, 100,childSpeed,CorspeRef);
        }

        float NextBirthingTime = Random.Range(60.0f, 121.0f);
        Invoke("Birthing", NextBirthingTime);
    }



    void FoodDrain()
    {
        FoodCount = Mathf.Clamp(FoodCount - ReductionRate, 0, 100);
        if (FoodCount == 0)
        {
            m_Health -= 5;
            DeathCheck();
        }
    }

    void WaterDrain()
    {
        WaterCount = Mathf.Clamp(WaterCount - ReductionRate, 0, 100);
        if (WaterCount == 0)
        {
            m_Health -= 5;
            DeathCheck();
        }
    }

    public void TouchingWater()
    {

        if (m_State == AIStates.Finding_Water)
        {
            TargetLocation = this.transform.position;
            isMoving = false;
            m_State = AIStates.Drinking;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ChasingEntity && m_State != AIStates.Eating)
        {
            ChasingEntity = null;
        }
    }
   
    void DeathCheck()
    {
        if(m_Health <= 0)
        {
            Instantiate(CorspeRef, this.transform.position, this.transform.rotation);
            print("Name " + this.gameObject.name + " FoodCount " + FoodCount + " Water Count " + WaterCount);
            Destroy(this.gameObject);
        }
    }

}
