using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : Singleton<MainCamera>
{
    //Padding to prevent player out of the camera ==============================================================
    [SerializeField] private float paddingLeft = 0.5f;
    [SerializeField] private float paddingRight = 0.5f;
    [SerializeField] private float paddingTop = 2f;
    [SerializeField] private float paddingDown = 0.5f;


    // Bounds which cover the main camera ======================================================================
    private Vector2 minBounds;
    private Vector2 maxBounds;



    // Bounds which cover the area play can move ===============================================================
    private Vector2 minMoveableBounds;
    private Vector2 maxMoveableBounds;
    public Vector2 MinMoveableBounds {get{return minMoveableBounds;}}
    public Vector2 MaxMoveableBounds {get{return maxMoveableBounds;}}


    private void Awake() {
        Camera mainCamera = Camera.main;

        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0f,0f));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1f,1f));


        minMoveableBounds = new Vector2(minBounds.x + paddingLeft, minBounds.y + paddingDown);
        maxMoveableBounds = new Vector2(maxBounds.x - paddingRight, maxBounds.y - paddingTop);
    }
}
