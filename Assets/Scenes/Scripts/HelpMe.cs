using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 m_Position;
    private Quaternion m_Rotation;
    public float CameraSpeed;
    public float ScrollSpeed;

    void Start()
    {
        GameObject gameObject = GameObject.Find("MapManager");

        float Xpos = gameObject.GetComponent<SimulationStarter>().BoundX;
        float Zpos = gameObject.GetComponent <SimulationStarter>().BoundZ;

        m_Rotation = this.transform.rotation;
        m_Position = this.transform.position;
        m_Position.y = (Xpos + Zpos) / 4;
        print("POSY " + m_Position.y);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {

            m_Position += this.transform.forward * CameraSpeed / 50;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Position -= this.transform.forward * CameraSpeed / 50;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Position -= this.transform.right * CameraSpeed / 50;
        }
        if(Input.GetKey(KeyCode.D))
        {
            m_Position += this.transform.right * CameraSpeed / 50;
        }
        if(Input.GetKey(KeyCode.Q))
        {
           
            this.transform.RotateAround(this.transform.position, Vector3.down, CameraSpeed/ 100);
        }
        if(Input.GetKey(KeyCode.E))
        {
           
            this.transform.RotateAround(this.transform.position, Vector3.up, CameraSpeed / 100);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !Input.GetKey(KeyCode.LeftShift))
        {
            m_Position.y -= ScrollSpeed / 10;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && !Input.GetKey(KeyCode.LeftShift))
        {
            m_Position.y += ScrollSpeed / 10;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && Input.GetKey(KeyCode.LeftShift))
        {
            //this.gameObject.transform.RotateAround(this.transform.position, Vector3.left, CameraSpeed/ 2);
            this.GetComponentInChildren<Camera>().transform.RotateAround(this.transform.position, Vector3.left, CameraSpeed / 2);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && Input.GetKey(KeyCode.LeftShift))
        {
            //this.gameObject.transform.RotateAround(this.transform.position, Vector3.right, CameraSpeed /2);
            this.GetComponentInChildren<Camera>().transform.RotateAround(this.transform.position, Vector3.right, CameraSpeed / 2);
        }
        
        
        this.transform.position = m_Position;  
       
    }
}
