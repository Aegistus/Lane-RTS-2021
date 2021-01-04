using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTest : MonoBehaviour
{
    public Grid grid;

    private void Start()
    {
        grid.SetNodeWalkable(new Vector3(-1, 0, 7), false);
        grid.SetNodeWalkable(new Vector3(-2, 0, 7), false);
        grid.SetNodeWalkable(new Vector3(-3, 0, 7), false);
        grid.SetNodeWalkable(new Vector3(-4, 0, 7), false);
        grid.SetNodeWalkable(new Vector3(0, 0, 7), false);
        grid.SetNodeWalkable(new Vector3(1, 0, 7), false);
        grid.SetNodeWalkable(new Vector3(2, 0, 7), false);
        grid.SetNodeWalkable(new Vector3(3, 0, 7), false);
        //grid.SetNodeWalkable(new Vector3(4, 0, 7), false);
    }
}
