using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot 
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData item , int amount)
    {
        this.itemData = item;
        this.stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        this.itemData = null;
        this.stackSize = -1;
    }

    public void AddToStack(int amount){
        stackSize += amount;
    }

    public void RemoveFromStack(int amount){
        stackSize -= amount;
    }

    public bool RoomLeftInStack(int amount , out int amountRemaining){
        amountRemaining = itemData.MaxStackSize - stackSize;

        return RoomLeftInStack(amount);
    }

    public bool RoomLeftInStack(int amount){
        if (stackSize + amount <= itemData.MaxStackSize){
            return true;
        }
        else{
            return false;
        }


}


}