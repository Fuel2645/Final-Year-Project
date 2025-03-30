using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(SimulationStarter))]
public class SImulationInspectorScript : Editor
{
    SerializedProperty MapSize;
    SerializedProperty HerbivourCount;
    SerializedProperty HerbivourSpeed;
    SerializedProperty HerbivourHealth;

    SerializedProperty CarnivourCount;
    SerializedProperty CarnivourSpeed;
    SerializedProperty CarnivourHealth;

    SerializedProperty FoodSpawnerCount;
    SerializedProperty MinFoodDelay;
    SerializedProperty MaxFoodDelay;
    SerializedProperty MaxFoodCount;

    SerializedProperty initialWaterCount;
    SerializedProperty MinWaterDelay;
    SerializedProperty MaxWaterDelay;
    SerializedProperty MaxWaterCount;

    SerializedProperty BoundX;
    SerializedProperty BoundZ;


    void OnEnable()
    {
        MapSize = serializedObject.FindProperty("MapSize");
        HerbivourCount = serializedObject.FindProperty("HerbivourCount");
        HerbivourSpeed = serializedObject.FindProperty("HerbivourSpeed");
        HerbivourHealth = serializedObject.FindProperty("HerbivourHealth");

        CarnivourCount = serializedObject.FindProperty("CarnivourCount");
        CarnivourSpeed = serializedObject.FindProperty("CarnivourSpeed");
        CarnivourHealth = serializedObject.FindProperty("CarnivourHealth");

        FoodSpawnerCount = serializedObject.FindProperty("FoodSpawnerCount");
        MinFoodDelay = serializedObject.FindProperty("MinFoodDelay");
        MaxFoodDelay = serializedObject.FindProperty("MaxFoodDelay");
        MaxFoodCount = serializedObject.FindProperty("MaxFoodCount");

        initialWaterCount = serializedObject.FindProperty("initialWaterCount");
        MinWaterDelay = serializedObject.FindProperty("MinWaterDelay");
        MaxWaterDelay = serializedObject.FindProperty("MaxWaterDelay");
        MaxWaterCount = serializedObject.FindProperty("MaxWaterCount");

        BoundX = serializedObject.FindProperty("BoundX");
        BoundZ = serializedObject.FindProperty("BoundZ");


    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(MapSize);
        if (MapSize.enumValueIndex != 0)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(HerbivourCount);
            EditorGUILayout.PropertyField(HerbivourSpeed);
            EditorGUILayout.PropertyField(HerbivourHealth);
            EditorGUILayout.PropertyField(CarnivourCount);
            EditorGUILayout.PropertyField(CarnivourSpeed);
            EditorGUILayout.PropertyField(CarnivourHealth);
            EditorGUILayout.PropertyField(FoodSpawnerCount);
            EditorGUILayout.PropertyField(MinFoodDelay);
            EditorGUILayout.PropertyField(MaxFoodDelay);
            EditorGUILayout.PropertyField(MaxFoodCount);
            EditorGUILayout.PropertyField(initialWaterCount);
            EditorGUILayout.PropertyField(MinWaterDelay);
            EditorGUILayout.PropertyField(MaxWaterDelay);
            EditorGUILayout.PropertyField(MaxWaterCount);
            EditorGUILayout.PropertyField(BoundX);
            EditorGUILayout.PropertyField(BoundZ);
            EditorGUI.EndDisabledGroup();

        }
        else
        {
            EditorGUILayout.PropertyField(HerbivourCount);
            EditorGUILayout.PropertyField(HerbivourSpeed);
            EditorGUILayout.PropertyField(HerbivourHealth);
            EditorGUILayout.PropertyField(CarnivourCount);
            EditorGUILayout.PropertyField(CarnivourSpeed);
            EditorGUILayout.PropertyField(CarnivourHealth);
            EditorGUILayout.PropertyField(FoodSpawnerCount);
            EditorGUILayout.PropertyField(MinFoodDelay);
            EditorGUILayout.PropertyField(MaxFoodDelay);
            EditorGUILayout.PropertyField(MaxFoodCount);
            EditorGUILayout.PropertyField(initialWaterCount);
            EditorGUILayout.PropertyField(MinWaterDelay);
            EditorGUILayout.PropertyField(MaxWaterDelay);
            EditorGUILayout.PropertyField(MaxWaterCount);
            EditorGUILayout.PropertyField(BoundX);
            EditorGUILayout.PropertyField(BoundZ);
        }
        switch (MapSize.enumValueIndex)
        {
            case 0: //Custom


                break;
            case 1: //Small
                HerbivourCount.intValue = 20;
                HerbivourSpeed.floatValue = 20;
                HerbivourHealth.intValue = 100;
                CarnivourCount.intValue = 10;
                CarnivourSpeed.floatValue = 25;
                CarnivourHealth.intValue = 100;
                FoodSpawnerCount.intValue = 4;
                MinFoodDelay.floatValue = 0.5f;
                MaxFoodDelay.floatValue = 2.0f;
                MaxFoodCount.intValue = 100;
                initialWaterCount.intValue = 1;
                MinWaterDelay.intValue = 60;
                MaxWaterDelay.intValue = 120;
                MaxWaterCount.intValue = 3;
                BoundX.floatValue = 50.0f;
                BoundZ.floatValue = 50.0f;



                break;
            case 2: //Medium
                HerbivourCount.intValue = 40;
                HerbivourSpeed.floatValue = 20;
                HerbivourHealth.intValue = 100;
                CarnivourCount.intValue = 15;
                CarnivourSpeed.floatValue = 25;
                CarnivourHealth.intValue = 100;
                FoodSpawnerCount.intValue = 5;
                MinFoodDelay.floatValue = 0.5f;
                MaxFoodDelay.floatValue = 2.0f;
                MaxFoodCount.intValue = 150;
                initialWaterCount.intValue = 2;
                MinWaterDelay.intValue = 60;
                MaxWaterDelay.intValue = 120;
                MaxWaterCount.intValue = 5;
                BoundX.floatValue = 70.0f;
                BoundZ.floatValue = 70.0f;

                break;
            case 3: //Large
                HerbivourCount.intValue = 70;
                HerbivourSpeed.floatValue = 20;
                HerbivourHealth.intValue = 100;
                CarnivourCount.intValue = 30;
                CarnivourSpeed.floatValue = 25;
                CarnivourHealth.intValue = 100;
                FoodSpawnerCount.intValue = 5;
                MinFoodDelay.floatValue = 0.5f;
                MaxFoodDelay.floatValue = 2.0f;
                MaxFoodCount.intValue = 200;
                initialWaterCount.intValue = 3;
                MinWaterDelay.intValue = 60;
                MaxWaterDelay.intValue = 120;
                MaxWaterCount.intValue = 6;
                BoundX.floatValue = 100.0f;
                BoundZ.floatValue = 100.0f;
                break;
            case 4: //Enormous
                HerbivourCount.intValue = 150;
                HerbivourSpeed.floatValue = 20;
                HerbivourHealth.intValue = 100;
                CarnivourCount.intValue = 50;
                CarnivourSpeed.floatValue = 25;
                CarnivourHealth.intValue = 100;
                FoodSpawnerCount.intValue = 15;
                MinFoodDelay.floatValue = 0.5f;
                MaxFoodDelay.floatValue = 2.0f;
                MaxFoodCount.intValue = 400;
                initialWaterCount.intValue = 5;
                MinWaterDelay.intValue = 60;
                MaxWaterDelay.intValue = 120;
                MaxWaterCount.intValue = 10;
                BoundX.floatValue = 200.0f;
                BoundZ.floatValue = 200.0f;
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }
}
