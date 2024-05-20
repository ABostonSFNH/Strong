using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class VoronoiGenerator : MonoBehaviour
{
    public Texture2D tex;
    public MeshRenderer visualizer;

    public int size;
    public int seedNum;

    public Vector2Int[,] pixels;
    public Color[] colors;
    public List<Vector2Int> seeds = new List<Vector2Int>();

    // Start is called before the first frame update
    void Start()
    {
        tex = new Texture2D(size, size);
        tex.filterMode = FilterMode.Point;
        pixels = new Vector2Int[size, size];
        visualizer.material.mainTexture = tex;
        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                pixels[x,y] = new Vector2Int(x, y);
            }
        }

        GenerateNewMap();
    }

    public void GenerateNewMap()
    {
        for(int s = 0; s < seedNum; s++)
        {
            seeds.Add(new Vector2Int(Random.Range(0, size), Random.Range(0, size)));
        }

        foreach(Vector2Int seed in seeds)
        {
            tex.SetPixel(seed.x, seed.y, colors[Random.Range(0, colors.Length)]);
        }

        foreach(Vector2Int pixel in pixels)
        {
            float dist = float.MaxValue;
            Vector2Int seedCoords = seeds[0];

            foreach(Vector2Int seed in seeds)
            {
                if(Vector2.Distance(pixel, seed) < dist)
                {
                    dist = Vector2.Distance(pixel, seed);
                    seedCoords = seed;
                }
            }

            tex.SetPixel(pixel.x, pixel.y, tex.GetPixel(seedCoords.x, seedCoords.y));
        }

        tex.Apply();
    }
}
