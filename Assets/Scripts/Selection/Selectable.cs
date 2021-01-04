using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Selectable : MonoBehaviour
{
    public bool Selected { get; private set; }

    private Camera mainCam;
    private SingleSelection singleSelect;
    private BoxSelection boxSelect;

    //private Material selectedMat;
    public List<GameObject> selectionMarkers;

    private void Start()
    {
        mainCam = Camera.main;
        singleSelect = SingleSelection.instance;
        boxSelect = BoxSelection.instance;
        singleSelect.OnAddSelection += Select;
        singleSelect.OnClearSelection += Deselect;
        boxSelect.OnBoxSelection += CheckBoxLocation;
        boxSelect.OnClearSelection += Deselect;

        foreach (var marker in selectionMarkers)
        {
            marker.SetActive(false);
        }
    }

    // precondition: selectBox is a valid Rectangle
    // postcondition: if this gameObject is within the rectangle, Select() is called
    //                      otherwise, Deselect() (if ctrl is not held down)
    public void CheckBoxLocation(Rect selectBox)
    {
        if (selectBox.Contains(mainCam.WorldToViewportPoint(transform.position), true) && !Selected)
        {
            Select();
        }
        else if (!selectBox.Contains(mainCam.WorldToViewportPoint(transform.position), true) && Selected)
        {
            if (!Input.GetKey("left ctrl"))
            {
                Select();
            }
        }
    }

    // precondition: unit is not selected.
    // postcondition: selected is set to true, selection markers are activated.
    public void Select(GameObject toSelect)
    {
        if (toSelect == this.gameObject)
        {
            Selected = true;
            // activates selection markers
            foreach (GameObject marker in selectionMarkers)
            {
                marker.SetActive(true);
            }
            SelectListeners();
        }
    }

    // overload with zero parameters. Currently unused.
    public void Select()
    {
        Selected = true;
        // activates selection markers
        foreach (GameObject marker in selectionMarkers)
        {
            marker.SetActive(true);
        }
        SelectListeners();
    }

    // precondition: unit is not selected.
    // postcondition: selected is set to false, selection markers are deactivated.
    public void Deselect(GameObject toDeselect)
    {
        if (toDeselect == this.gameObject)
        {
            Selected = false;
            // deactivates selection markers
            foreach (GameObject marker in selectionMarkers)
            {
                marker.SetActive(false);
            }
            DeselectListeners();
        }
    }

    // overload with zero parameters. Used with onClearSelection.
    public void Deselect()
    {
        Selected = false;
        // deactivates selection markers
        foreach (GameObject marker in selectionMarkers)
        {
            marker.SetActive(false);
        }
        DeselectListeners();
    }

    // Helper method to manage subscriptions. For when Selected == true.
    private void SelectListeners()
    {

    }

    // Helper method to manage subscriptions. For when Selected == false.
    private void DeselectListeners()
    {

    }

    // unsubscribes from all events. REQUIRED TO AVOID MEMORY LEAKS.
    private void OnDestroy()
    {
        singleSelect.OnAddSelection -= Select;
        singleSelect.OnClearSelection -= Deselect;
        boxSelect.OnBoxSelection -= CheckBoxLocation;
        boxSelect.OnClearSelection -= Deselect;
    }
}


