using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(StageListSO))]
public class StageListEditor : Editor
{
    private SerializedProperty stageListProperty;
    private bool[] foldouts;
    private void OnEnable()
    {
        stageListProperty = serializedObject.FindProperty("stageList");
        foldouts = new bool[stageListProperty.arraySize];
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (stageListProperty != null && stageListProperty.isArray)
        {
            if (foldouts.Length != stageListProperty.arraySize)
            {
                foldouts = new bool[stageListProperty.arraySize];
            }

            for (int i = 0; i < stageListProperty.arraySize; i++)
            {
                SerializedProperty stageProperty = stageListProperty.GetArrayElementAtIndex(i);
                SerializedProperty flyModeProperty = stageProperty.FindPropertyRelative("flyMode");
                SerializedProperty pathConfigSOListProperty = stageProperty.FindPropertyRelative("pathConfigSOList");
                SerializedProperty shapeConfigSOProperty = stageProperty.FindPropertyRelative("shapeConfigSO");
                SerializedProperty enemyAmountProperty = stageProperty.FindPropertyRelative("enemyAmount");

                foldouts[i] = EditorGUILayout.Foldout(foldouts[i], $"Stage {i + 1}");
                if (foldouts[i])
                {
                    EditorGUILayout.PropertyField(flyModeProperty);

                    if ((FlyMode)flyModeProperty.enumValueIndex == FlyMode.FollowALine)
                    {
                        EditorGUILayout.PropertyField(pathConfigSOListProperty);
                        EditorGUILayout.PropertyField(enemyAmountProperty);
                    }
                    else if ((FlyMode)flyModeProperty.enumValueIndex == FlyMode.ToShapeConfig)
                    {
                        EditorGUILayout.PropertyField(shapeConfigSOProperty);
                    }
                }
                EditorGUILayout.Space();
            }

            if (GUILayout.Button("Add Stage"))
            {
                stageListProperty.InsertArrayElementAtIndex(stageListProperty.arraySize);
                Array.Resize(ref foldouts, stageListProperty.arraySize);
            }

            if (GUILayout.Button("Remove Last Stage"))
            {
                if (stageListProperty.arraySize > 0)
                {
                    stageListProperty.DeleteArrayElementAtIndex(stageListProperty.arraySize - 1);
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
