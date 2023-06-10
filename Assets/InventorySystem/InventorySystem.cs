using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public int inventorySize => InventorySlots.Count;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);
    
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }


    }

    public bool AddToInventory(InventoryItemData item , int amount)
    {
        inventorySlots[0] = new InventorySlot(item , amount);
        return true;

}
}