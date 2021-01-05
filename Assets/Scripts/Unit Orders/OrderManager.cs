using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

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
    }

    private List<OrderReceiver> orderListeners = new List<OrderReceiver>();
    private Grid grid;

    private void Start()
    {
        grid = (Grid)FindObjectOfType(typeof(Grid));
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit rayHit);
            Vector3 tileLocation = grid.NodeFromWorldPoint(rayHit.point).worldPosition;
            IssueOrder(tileLocation);
        }
    }

    public void AddOrderListener(OrderReceiver listener)
    {
        orderListeners.Add(listener);
    }

    public void RemoveOrderListener(OrderReceiver listener)
    {
        orderListeners.Remove(listener);
    }

    public void IssueOrder(Vector3 position)
    {
        foreach (var listener in orderListeners)
        {
            listener.ReceiveOrder(position);
        }
    }
}
