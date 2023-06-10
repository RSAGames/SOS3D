using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickUp : MonoBehaviour
{
    
    public InventoryItemData itemData;

    private Collider Collider;

    private void Awake()
    {
        Collider = GetComponent<Collider>();
        Collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (!inventory)
        {
            return;
        }

        if (inventory.InventorySystem.AddToInventory(itemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
