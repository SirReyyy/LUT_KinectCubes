using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CubesIndexCoordinates3 : MonoBehaviour
{
    private CubesIndexScript3 cubeIndexScript;
    public GameObject cubePrefabs;
    private GameObject cubeInstance;

    public int rows = 4; // Number of rows
    public int cols = 4; // Number of columns

    void Awake() {
        SpawnCubes();
    } //-- Awake end

    void Start() {
        IdentifyInGrid();
    } //-- Start end

    private void SpawnCubes2() {
        int totalCubes = rows * cols;

        for (int i = 0; i < totalCubes; i++) {
            cubeInstance = Instantiate(cubePrefabs);
            cubeInstance.transform.parent = transform;
        }
    } //--- SpawnCubes end

    private void SpawnCubes()
    {
        float xSpacing = 12f / cols;  // Calculate the spacing between cubes along x-axis
        float ySpacing = 8f / rows;   // Calculate the spacing between cubes along y-axis

        float minSpacing = Mathf.Min(xSpacing, ySpacing); // Find the minimum spacing between rows and columns

        float scaleFactor = minSpacing * 0.08f; // Calculate the scale factor for proper separation
        Vector3 cubeScale = new Vector3(scaleFactor, scaleFactor, scaleFactor); // Set the cube scale

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                float xPos = -6f + xSpacing * col + xSpacing / 2f; // Calculate the x position of the cube
                float yPos = 4f - ySpacing * row - ySpacing / 2f; // Calculate the y position of the cube

                Vector3 position = new Vector3(xPos, yPos, 0f);
                cubeInstance = Instantiate(cubePrefabs, position, Quaternion.identity);
                cubeInstance.transform.parent = transform;
                cubeInstance.transform.localScale = cubeScale; // Set the cube scale
            }
        }
    }






    private void IdentifyInGrid() {
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

            cubeIndexScript = transform.GetChild(i).GetComponent<CubesIndexScript3>();
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

