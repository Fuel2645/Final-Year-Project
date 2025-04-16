using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fps : MonoBehaviour
{
    private float count;
    public GameObject selectedObject; 


    private IEnumerator Start()
    {
        GUI.depth = 2; 
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5, 40, 100, 25), "FPS: " + Mathf.Round(count));
        GUIStyle gUI = new GUIStyle();
        gUI.normal.textColor = Color.green;
        GUI.Label(new Rect(300, 40, 200, 25), "Herbivore: " + GameObject.FindGameObjectsWithTag("Herbivore").Count(), gUI);
        gUI.normal.textColor = Color.red;

        GUI.Label(new Rect(600, 40, 200, 25), "Carnivore: " + GameObject.FindGameObjectsWithTag("Carnivore").Count(), gUI);


        gUI.normal.textColor = Color.white;
        if(selectedObject != null) 
        {
            if(selectedObject.tag == "Carnivore")
            {
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 2, 200, 25), "Carnivore");
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 3, 200, 25), "Health " + selectedObject.GetComponent<CarnivoreScript>().m_Health);
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 4, 200, 25), "Hunger " + selectedObject.GetComponent<CarnivoreScript>().FoodCount);
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 5, 200, 25), "Thirst " + selectedObject.GetComponent<CarnivoreScript>().WaterCount);
            }
            else if(selectedObject.tag == "Herbivore")
            {
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 2, 200, 25), "Herbivore");
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 3, 200, 25), "Health " + selectedObject.GetComponent<herbivorStuff>().m_Health);
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 4, 200, 25), "Hunger " + selectedObject.GetComponent<herbivorStuff>().FoodCount);
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 5, 200, 25), "Thirst " + selectedObject.GetComponent<herbivorStuff>().WaterCount);
            }
            else if (selectedObject.tag == "Water")
            {
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 4, 200, 25), "Lake");
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 5, 200, 25), "Water Left " + selectedObject.GetComponent<WaterScript>().WaterCount);
            }
            else if (selectedObject.tag == "Corpse")
            {
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 4, 200, 25), "Corpsewa");
                GUI.Label(new Rect(Screen.width / 5 * 4, Screen.height / 6 * 5, 200, 25), "Meat Left " + selectedObject.GetComponent<CorspseScript>().Health);
            }
        }

       



    }
}
