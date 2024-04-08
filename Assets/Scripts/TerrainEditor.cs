using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainEditor : MonoBehaviour
{

    [SerializeField] private TerrainData terrainData;
    [SerializeField] private Terrain terrain;

    [SerializeField] private int seed = 0;
    [SerializeField] private int detail = 150;
    [SerializeField] private TerrainPaint paint;
    [SerializeField] private int paintNumberTrees = 50;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float minZ = 0f;
    [SerializeField] private float maxX = 100f;
    [SerializeField] private float maxZ = 100f;




    private float[,] matrix;

    // Start is called before the first frame update
    void Start()
    {

        matrix = new float[513, 513];
        HeightGenerator();
        terrainData.SetHeights(0, 0, matrix);


        paint.TerrainPainting();

        PaintTrees();

    }

    private void PaintTrees()
    {
        for (int i = 0; i < paintNumberTrees; i++)
        {
            TreeInstance tree = new TreeInstance();
            tree = terrainData.treeInstances[i];
            Vector3 position = new Vector3(Random.Range(minX,maxX), 0, Random.Range(minZ,maxZ));
            tree.position = position;
            terrain.AddTreeInstance(tree);
        }
    }

    public void HeightGenerator()
    {
        for (int i = 0; i < matrix.GetLength(0); i++) {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                //0f seria 0 y 1f seria 600
                //matrix[i,j] = Random.Range(0.0f, 0.0025f);


                matrix[i, j] = Mathf.PerlinNoise((float)(i+seed)/detail, (float)(j+seed)/detail);
            }
        }
    }
}
