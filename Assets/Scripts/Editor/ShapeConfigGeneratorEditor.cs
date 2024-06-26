using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapeConfigGenerator))]
public class ShapeConfigGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Debug.Log("Editor is running");
        ShapeConfigGenerator shapeConfigGenerator = (ShapeConfigGenerator)target;

        if (GUILayout.Button("Generate Shape Config"))
        {
            shapeConfigGenerator.GenerateShapeConfig();
        }
    }
}
