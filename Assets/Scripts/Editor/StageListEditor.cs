using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

[CustomEditor(typeof(StageListSO))]
public class StageListEditor : Editor
{
    private SerializedProperty stageListProperty;
    private ReorderableList reorderableList;

    private void OnEnable()
    {
        stageListProperty = serializedObject.FindProperty("stageList");

        reorderableList = new ReorderableList(serializedObject, stageListProperty, true, true, true, true)
        {
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Stages"),
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                SerializedProperty element = stageListProperty.GetArrayElementAtIndex(index);
                SerializedProperty flyModeProperty = element.FindPropertyRelative("flyMode");
                SerializedProperty pathConfigSOListProperty = element.FindPropertyRelative("pathConfigSOList");
                SerializedProperty shapeConfigSOProperty = element.FindPropertyRelative("shapeConfigSO");
                SerializedProperty enemyAmountProperty = element.FindPropertyRelative("enemyAmount");

                rect.y += 2;
                float lineHeight = EditorGUIUtility.singleLineHeight;
                float spacing = 2f;

                // foldouts[index] = EditorGUI.Foldout(new Rect(rect.x, rect.y, rect.width, lineHeight), foldouts[index], $"Stage {index + 1}", true);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), flyModeProperty);
                rect.y += lineHeight + spacing;

                if ((FlyMode)flyModeProperty.enumValueIndex == FlyMode.FollowALine)
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), pathConfigSOListProperty, true);
                    rect.y += EditorGUI.GetPropertyHeight(pathConfigSOListProperty, true) + spacing;
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), enemyAmountProperty);
                }
                else if ((FlyMode)flyModeProperty.enumValueIndex == FlyMode.ToShapeConfig)
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), shapeConfigSOProperty);
                }
            },
            elementHeightCallback = index =>
            {
                SerializedProperty element = stageListProperty.GetArrayElementAtIndex(index);
                SerializedProperty flyModeProperty = element.FindPropertyRelative("flyMode");

                float lineHeight = EditorGUIUtility.singleLineHeight;
                float spacing = 2f;
                float height = lineHeight + spacing;

                if ((FlyMode)flyModeProperty.enumValueIndex == FlyMode.FollowALine)
                {
                    SerializedProperty pathConfigSOListProperty = element.FindPropertyRelative("pathConfigSOList");
                    height += EditorGUI.GetPropertyHeight(pathConfigSOListProperty, true) + spacing;
                    height += lineHeight + spacing;
                }
                else if ((FlyMode)flyModeProperty.enumValueIndex == FlyMode.ToShapeConfig)
                {
                    height += lineHeight + spacing;
                }

                return height;
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
