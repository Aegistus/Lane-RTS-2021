﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGrid : MonoBehaviour
{
    public static GroundGrid current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public int xLength = 10;
    public int zLength = 10;
    public Vector3 gridMiddle;
    public float tileSize = 1f;

    private GroundTile[,] tiles;
    private GameObject tileParent;

    private void Start()
    {
        CreateGrid(xLength, zLength);
        OccupyTile(gridMiddle);
    }

    private void CreateGrid(int xLength, int zLength)
    {
        tileParent = new GameObject("Tiles");
        tiles = new GroundTile[xLength, zLength];
        for (int x = 0; x < xLength; x++)
        {
            for (int z = 0; z < zLength; z++)
            {
                tiles[x, z] = new GameObject().AddComponent<GroundTile>();
                tiles[x, z].transform.position = new Vector3(gridMiddle.x - (xLength * tileSize / 2) + (x * tileSize), gridMiddle.y, gridMiddle.z - (zLength * tileSize / 2) + (z * tileSize));
                tiles[x, z].transform.parent = tileParent.transform;
                tiles[x, z].SetOccupied(true);
            }
        }
    }

    public void OccupyTile(Vector3 position)
    {
        int xCoord = Mathf.RoundToInt((gridMiddle.x - position.x) / tileSize);
        int zCoord = Mathf.RoundToInt((gridMiddle.z - position.z) / tileSize);
        tiles[xCoord, zCoord].SetOccupied(true);
    }

    public void DeOccupyTile(Vector3 position)
    {
        int xCoord = Mathf.RoundToInt((gridMiddle.x - position.x) / tileSize);
        int zCoord = Mathf.RoundToInt((gridMiddle.z - position.z) / tileSize);
        tiles[xCoord, zCoord].SetOccupied(false);
    }

    public bool CheckTileOccupancy(Vector3 position)
    {
        int xCoord = Mathf.RoundToInt((gridMiddle.x - position.x) / tileSize);
        int zCoord = Mathf.RoundToInt((gridMiddle.z - position.z) / tileSize);
        return tiles[xCoord, zCoord].Occupied;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 xStart = new Vector3(gridMiddle.x - (xLength * tileSize / 2), gridMiddle.y, gridMiddle.z - (zLength * tileSize / 2));
        Vector3 xEnd = new Vector3(gridMiddle.x + (xLength * tileSize / 2), gridMiddle.y, gridMiddle.z - (zLength * tileSize / 2));
        for (int i = 0; i <= zLength; i++)
        {
            Gizmos.DrawLine(xStart, xEnd);
            xStart.z += tileSize;
            xEnd.z += tileSize;
        }
        Vector3 zStart = new Vector3(gridMiddle.x - (xLength * tileSize / 2), gridMiddle.y, gridMiddle.z - (zLength * tileSize / 2));
        Vector3 zEnd = new Vector3(gridMiddle.x - (xLength * tileSize / 2), gridMiddle.y, gridMiddle.z + (zLength * tileSize / 2));
        for (int i = 0; i <= xLength; i++)
        {
            Gizmos.DrawLine(zStart, zEnd);
            zStart.x += tileSize;
            zEnd.x += tileSize;
        }
    }
}
