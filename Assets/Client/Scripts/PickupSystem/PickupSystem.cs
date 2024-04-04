using Inventory;
using UnityEngine;

namespace PickupSystem
{
    public class PickupSystem : MonoBehaviour
    {
        [SerializeField] private InventorySO _inventoryData;

        private void OnTriggerEnter2D(Collider2D col)
        {
            Item item = col.GetComponent<Item>();
            if (item != null)
            {
                int reminder = _inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
        }
    }
}