using UnityEngine;
using System.Collections.Generic;
public class Inventory_UI : MonoBehaviour
{
    public List<Inventory_Slot> inventorySlotsUI;
    public Inventory inventory;
    public void UpdateUI(List<InventorySlot> slots)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i].itemType == InventorySlot.Item.Crucifix)
            {
                inventorySlotsUI[i].text.text = ((int)slots[i].charge).ToString();
            }
            else
            {
                inventorySlotsUI[i].text.text = slots[i].amount.ToString();
            }


            if (inventory.currentlySelectedSlot != null)
            {

                if (inventory.currentlySelectedSlot.itemType != InventorySlot.Item.None && slots[i].index == inventory.currentlySelectedSlot.index)
                {
                    inventorySlotsUI[i].background.color = Color.red;
                }
                else
                {
                    inventorySlotsUI[i].background.color = Color.white;
                }

            }
            else
            {
                inventorySlotsUI[i].background.color = Color.white;
            }
        }


    }
}
