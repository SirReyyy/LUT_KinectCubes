using System.Collections.Generic;
using UnityEngine;

public class TextureDivider : MonoBehaviour
{
    public int rows = 3;
    public int columns = 3;
    public GameObject cubePrefab;

    private List<Texture2D> dividedTextures = new List<Texture2D>();

    void Start()
    {
        // Get all assigned textures
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Texture2D texture = renderer.material.mainTexture as Texture2D;
            if (texture != null)
            {
                DivideTexture(texture);
            }
        }

        // Place cut-out textures on cubes
        int textureIndex = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject cube = Instantiate(cubePrefab, new Vector3(i, 0, j), Quaternion.identity);
                cube.GetComponent<Renderer>().material.mainTexture = dividedTextures[textureIndex++];
            }
        }
    }

    void DivideTexture(Texture2D texture)
    {
        int width = texture.width / rows;
        int height = texture.height / columns;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Texture2D cutTexture = new Texture2D(width, height);
                cutTexture.SetPixels(texture.GetPixels(i * width, j * height, width, height));
                cutTexture.Apply();
                dividedTextures.Add(cutTexture);
            }
        }
    }
}
