using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SingleSelection : MonoBehaviour
{
    public static SingleSelection instance;

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

    public event Action OnClearSelection;
    public event Action<GameObject> OnAddSelection;

    [SerializeField]
    private LayerMask selectablesLayer;

    private Vector3 mousePos1;
    //private Vector3 mousePos2;

    private Camera mainCam;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RecordMousePos(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            SingleSelect(FindClickInfo(), Input.mousePosition);
        }
    }

    // records the first mousePos from onLeftClickStart for comparison later
    private void RecordMousePos(Vector3 mousePos1)
    {
        this.mousePos1 = mousePos1;
    }

    // selects the single unit that the raycast hit
    // if ctrl is not being held down, clears selection first
    private void SingleSelect(RaycastHit rayHit, Vector3 mousePos2)
    {
        if (mousePos1 == mousePos2 && rayHit.collider != null)
        {
            GameObject collidedObj = rayHit.collider.gameObject;
            if (collidedObj.GetComponent<Selectable>() != null)
            {
                if (!Input.GetKey("left ctrl")) // if ctrl is not held down
                {
                    OnClearSelection?.Invoke();
                }
                OnAddSelection?.Invoke(collidedObj);
            }
            else if (!Input.GetKey("left ctrl")) // if the raycast doesn't hit a selectable object and left ctrl is not down
            {
                OnClearSelection?.Invoke();
            }
        }
    }

    private RaycastHit FindClickInfo()
    {
        Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit rayHit, Mathf.Infinity);
        return rayHit;
    }
}
