using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoxSelection : MonoBehaviour
{
    public static BoxSelection instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        mainCam = Camera.main;
    }

    public event Action<Rect> OnBoxSelection;
    public event Action OnClearSelection;

    [SerializeField]
    private LayerMask selectablesLayer;
    public float checkInterval;

    private Vector3 mousePos1;
    private Vector3 mousePos2;
    private Rect selectRect;
    private Camera mainCam;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginBoxSelection(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            ContinueBoxSelection(FindClickInfo(), Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndBoxSelection(Input.mousePosition);
        }
    }

    // called when left mouse is first pressed down, creates a new rectangle and passes it to 
    // BoxSelectionCheck. Clears current selection if left ctrl not down.
    public void BeginBoxSelection(Vector3 initMousePos)
    {
        // if ctrl is not being held down
        if (!Input.GetKey("left ctrl"))
        {
            OnClearSelection?.Invoke();
        }
        this.mousePos1 = initMousePos;
        this.mousePos2 = initMousePos;
        selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);
        OnBoxSelection?.Invoke(selectRect);
    }

    // called while left click remains down, creates a new rectangle for BoxSelectionCheck.
    // (commented out code edits existing rectangle. Doesn't work for some reason. Might change later for performance?)
    public void ContinueBoxSelection(RaycastHit rayHit, Vector3 mousePos2)
    {
        this.mousePos2 = mousePos2;
        selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);
        OnBoxSelection?.Invoke(selectRect);
    }

    // called when left click released, creates a new rectangle for BoxSelectionCheck.
    // (commented out code edits existing rectangle. Doesn't work for some reason. Might change later for performance?)
    public void EndBoxSelection(Vector3 mousePos2)
    {
        this.mousePos2 = mousePos2;
        selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);
        OnBoxSelection?.Invoke(selectRect);
    }

    private RaycastHit FindClickInfo()
    {
        RaycastHit rayHit;
        Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity);
        return rayHit;
    }
}
