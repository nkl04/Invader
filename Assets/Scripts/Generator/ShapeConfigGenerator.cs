using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum Shape{
    Triangle,
    Rectangle,
}
public class ShapeConfigGenerator : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField] private Shape shape;
    
    [Header("Shape Config Settings")]
    [SerializeField] private int rows = 5; 
    [SerializeField] private int columns = 9; 
    [SerializeField] [Range(0.5f,3f)] float rowSpacing = 1.0f; 
    [SerializeField] [Range(0.5f,3f)] float colSpacing = 1.0f;  


    public void GenerateShapeConfig()
    {
        // Tạo GameObject cha
        GameObject parentObject = new("Shape");
        parentObject.transform.position = Vector3.zero;
        
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(col * colSpacing, row * -rowSpacing, 0);
                GameObject child = Instantiate(pointPrefab.gameObject, position, Quaternion.identity);
                
                //change the icon of child
                GUIContent icon = EditorGUIUtility.IconContent("sv_icon_dot13_pix16_gizmo");
                EditorGUIUtility.SetIconForObject(child,icon.image as Texture2D);

                child.transform.parent = parentObject.transform;
            }
        }
    }
}
