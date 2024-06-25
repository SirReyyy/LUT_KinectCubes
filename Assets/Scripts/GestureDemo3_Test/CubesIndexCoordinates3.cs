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

    public List<Texture2D> mainTextures;
    public List<Texture> dividedTextures = new List<Texture>();
    Texture cutTexture;


    public int rows = 15; // Number of rows
    public int cols = 20; // Number of columns

    void Awake() {
        SpawnCubes();
    } //-- Awake end

    void Start() {
        IdentifyInGrid();
        DivideTextureLoop();
    } //-- Start end

    private void SpawnCubes()
    {
        float xSpacing = 16f / cols;  // Calculate the spacing between cubes along x-axis
        float ySpacing = 10f / rows;   // Calculate the spacing between cubes along y-axis

        float minSpacing = Mathf.Min(xSpacing, ySpacing); // Find the minimum spacing between rows and columns

        float scaleFactor = minSpacing * 0.08f; // Calculate the scale factor for proper separation
        Vector3 cubeScale = new Vector3(scaleFactor, scaleFactor, scaleFactor); // Set the cube scale

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                float xPos = -8f + xSpacing * col + xSpacing / 2f; // Calculate the x position of the cube
                float yPos = 5f - ySpacing * row - ySpacing / 2f; // Calculate the y position of the cube

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
                cubeIndexScript.cubeRowId = row;
                cubeIndexScript.cubeColId = col;
            }
        }
    } //-- IdentifyInGrid end

    
    private void DivideTextureLoop() {
        for (int t = 0; t < mainTextures.Count; t++) {
            DivideTexture(mainTextures[t]);
        }
    }

    public void DivideTexture(Texture2D originalTexture)
    {
        int width = originalTexture.width / cols;
        int height = originalTexture.height / rows;

        // Ensure the originalTexture is readable
        RenderTexture tempRT = RenderTexture.GetTemporary(
            originalTexture.width,
            originalTexture.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear
        );

        Graphics.Blit(originalTexture, tempRT);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Rect rect = new Rect(x * width, y * height, width, height);
                Texture2D newTexture = CreateTexture(tempRT, rect);
                dividedTextures.Add(newTexture);
                
                int index = dividedTextures.Count - 1;
                cubeIndexScript = transform.GetChild(index).GetComponent<CubesIndexScript3>();
                cubeIndexScript.slideTextures.Add(newTexture);

            }
        }

        dividedTextures.Clear();
        RenderTexture.ReleaseTemporary(tempRT);
    }

    private Texture2D CreateTexture(RenderTexture renderTexture, Rect rect)
    {
        Texture2D newTexture = new Texture2D((int)rect.width, (int)rect.height);
        RenderTexture.active = renderTexture;
        newTexture.ReadPixels(rect, 0, 0);
        newTexture.Apply();
        return newTexture;
    }
} //-- class end


/*
Project: 
Made by: 
*/

