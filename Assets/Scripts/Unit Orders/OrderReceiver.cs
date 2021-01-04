using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderReceiver : MonoBehaviour
{
    private OrderManager manager;
    private Selectable selectable;
    private UnitMovement movement;

    private void Start()
    {
        manager = OrderManager.instance;
        selectable = GetComponent<Selectable>();
        movement = GetComponent<UnitMovement>();

        selectable.OnSelectionStatusChange += ChangeSelectedStatus;
    }

    private void ChangeSelectedStatus(bool selected)
    {
        if (selected)
        {
            manager.AddOrderListener(this);
        }
        else
        {
            manager.RemoveOrderListener(this);
        }
    }

    public void ReceiveOrder(Vector3 position)
    {
        movement.SetDestination(position);
    }

    private void OnDestroy()
    {
        selectable.OnSelectionStatusChange -= ChangeSelectedStatus;
    }
}
