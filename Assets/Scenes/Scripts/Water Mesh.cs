using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMesh : MonoBehaviour
{
    Vector3[] newVerticies = new Vector3[]
    {   new Vector3(1.0f,0.0f,0.0f),
        new Vector3(0.0f,0.0f,0.5f),
        new Vector3(1.0f,0.0f,0.5f),
        new Vector3(2.0f,0.0f,0.5f),
        new Vector3(0.0f,0.0f,1.5f),
        new Vector3(1.0f,0.0f,1.5f),
        new Vector3(2.0f,0.0f,1.5f),
        new Vector3(1.0f,0.0f,2.0f)
    };




    int[] newTriagnles = new int[]
    { 
       0, 2 ,1,
       0,3,2,
       2, 3 ,6,
       2, 6 ,5,
       1, 2, 5,
       1,5,4,
       5,6,7,
       4,5,7,
    };


    Vector2[] newUV;
    

    // Start is called before the first frame update
    void Start()
    {
       // print("Existing");
    }


    private void OnEnable()
    {
        Mesh mesh = new Mesh()
        { name = "River"};
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        mesh.vertices = newVerticies;
        mesh.triangles = newTriagnles;


    }


}
