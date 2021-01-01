using System;
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

    private bool[,] tileOccupationStatus;

    private void Start()
    {
        CreateGrid(xLength, zLength);
    }

    private void CreateGrid(int xLength, int zLength)
    {
        tileOccupationStatus = new bool[xLength, zLength];
    }

    public void OccupyTile(Vector3 position)
    {
        int xCoord = (int)((gridMiddle.x - position.x) / tileSize);
        int zCoord = (int)((gridMiddle.z - position.z) / tileSize);
        tileOccupationStatus[xCoord, zCoord] = true;
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
