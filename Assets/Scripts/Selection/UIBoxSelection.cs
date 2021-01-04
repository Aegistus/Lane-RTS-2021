using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rtsV2
{
    public class UIBoxSelection : MonoBehaviour
    {
        // Note: needs to have a UI element (preferably image with box sprite) on the canvas.

        public GameObject selectionBox;

        private Vector3 mouseStart;
        private Vector3 mouseCurrent;
        private Vector3 midPoint;

        private float xScale;
        private float yScale;

        private void Start()
        {
            selectionBox.SetActive(false);
        }

        private void Update()
        {
            // when mouse button first pressed
            if (Input.GetMouseButtonDown(0))
            {
                mouseStart = Input.mousePosition;
                if (!selectionBox.activeInHierarchy)
                {
                    selectionBox.SetActive(true);
                }
            }
            // while mouse button is down
            if (Input.GetMouseButton(0))
            {
                mouseCurrent = Input.mousePosition;
                midPoint = (mouseCurrent + mouseStart) / 2;
                selectionBox.transform.position = midPoint;
                xScale = Mathf.Abs(mouseStart.x - mouseCurrent.x);
                yScale = Mathf.Abs(mouseStart.y - mouseCurrent.y);
                selectionBox.GetComponent<RectTransform>().sizeDelta = new Vector2(xScale, yScale);
            }
            // when mouse button is released
            if (Input.GetMouseButtonUp(0))
            {
                if (selectionBox.activeInHierarchy)
                {
                    selectionBox.SetActive(false);
                }
            }
        }
    }
}

