using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public bool Occupied { get; private set; }

    public void SetOccupied(bool occupied)
    {
        Occupied = occupied;
    }

    private void OnDrawGizmos()
    {
        if (Occupied)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, .1f);
        }
    }
}
