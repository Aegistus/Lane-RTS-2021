using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePreviewController : MonoBehaviour
{
    public Transform tilePreview;

    private Grid grid;
    private Camera cam;

    private void Start()
    {
        grid = (Grid)FindObjectOfType(typeof(Grid));
        cam = Camera.main;
    }

    private void Update()
    {
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit rayHit);
        tilePreview.position = grid.NodeFromWorldPoint(rayHit.point).worldPosition;
    }

}
