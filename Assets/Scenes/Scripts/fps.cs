using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fps : MonoBehaviour
{
    private float count;

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

    }
}
