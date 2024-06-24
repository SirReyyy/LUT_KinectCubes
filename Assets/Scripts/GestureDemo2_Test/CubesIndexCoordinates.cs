using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CubesIndexCoordinates : MonoBehaviour
{
    private CubesIndexScript cubeIndexScript;

    public int rows = 4; // Number of rows
    public int cols = 4; // Number of columns
    

    void Awake() {
        IdentifyInGrid();
    } //-- start end

    void IdentifyInGrid() {
        int childCount = transform.childCount;
        if (childCount != rows * cols)
        {
            Debug.LogError("The number of children does not match the grid dimensions");
            return;
        }

        for (int i = 0; i < childCount; i++)
        {
            // Calculate row and column
            int row = i / cols;
            int col = i % cols;

            cubeIndexScript = transform.GetChild(i).GetComponent<CubesIndexScript>();
            if(cubeIndexScript != null ) {
                // cubeIndexScript.cubeId = rowStr + colStr;
                cubeIndexScript.cubeRowId = row;
                cubeIndexScript.cubeColId = col;

                // transform.GetChild(i).AddComponent<CubesIndexListener>();
                // Debug.Log(cubeIndexScript.cubeId);
            }
        }
    } //-- IdentifyInGrid end


} //-- class end


/*
Project: 
Made by: 
*/

