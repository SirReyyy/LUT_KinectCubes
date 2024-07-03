using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinsIndexCoordinates : MonoBehaviour
{
    private CoinsIndexScript coinIndexScript;
    public GameObject coinPrefabs;
    private GameObject coinInstance;

    public int rows = 15; // Number of rows
    public int cols = 20; // Number of columns

    void Awake() {
        SpawnCubes();
    } //-- Awake end

    void Start() {
        IdentifyInGrid();
    } //-- Start end

    private void SpawnCubes()
    {
        float xSpacing = 17.8f / cols;  // Calculate the spacing between cubes along x-axis
        float ySpacing = 10f / rows;   // Calculate the spacing between cubes along y-axis

        float minSpacing = Mathf.Min(xSpacing, ySpacing); // Find the minimum spacing between rows and columns

        float scaleFactor = minSpacing * 0.8f; // Calculate the scale factor for proper separation
        float scaleFactorY = scaleFactor * 0.05f;
        Vector3 coinScale = new Vector3(scaleFactor, scaleFactorY, scaleFactor); // Set the cube scale

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                float xPos = -8.9f + xSpacing * col + xSpacing / 2f; // Calculate the x position of the cube
                float yPos = 5f - ySpacing * row - ySpacing / 2f; // Calculate the y position of the cube

                Vector3 position = new Vector3(xPos, yPos, 0f);
                coinInstance = Instantiate(coinPrefabs, position, Quaternion.Euler(90, 0, 0));
                coinInstance.transform.parent = transform;
                coinInstance.transform.localScale = coinScale; // Set the cube scale
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

            coinIndexScript = transform.GetChild(i).GetComponent<CoinsIndexScript>();
            if(coinIndexScript != null ) {
                coinIndexScript.coinRowId = row;
                coinIndexScript.coinColId = col;
            }
        }
    } //-- IdentifyInGrid end
} //-- class end


/*
Project: 
Made by: 
*/

