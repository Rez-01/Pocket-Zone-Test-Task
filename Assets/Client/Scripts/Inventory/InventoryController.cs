using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryPage _inventoryUI;
        [SerializeField] private InventorySO _inventoryData;

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            _inventoryData.Initialize();
            _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItemStruct> inventoryState)
        {
            _inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                _inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage,
                    item.Value.Quantity);
            }
        }

        private void PrepareUI()
        {
            _inventoryUI.InitializeInventoryUI(_inventoryData.Size);
            _inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItemStruct inventoryItemStruct = _inventoryData.GetItemAt(itemIndex);

            if (inventoryItemStruct.IsEmpty)
            {
                _inventoryUI.ResetSelection();
                return;
            }

            ItemSO itemSo = inventoryItemStruct.Item;

            _inventoryUI.UpdateDescription(itemIndex, itemSo.ItemImage, itemSo.name, itemSo.Description);
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItemStruct inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if (itemAction != null)
            {
                _inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
                _inventoryUI.ShowItemAction(itemIndex);
            }

            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null)
            {
                _inventoryUI.AddAction("Delete", () => DropItem(itemIndex, inventoryItem.Quantity));
            }
        }

        private void DropItem(int itemIndex, int inventoryItemQuantity)
        {
            _inventoryData.RemoveItem(itemIndex, inventoryItemQuantity);
            _inventoryUI.ResetSelection();
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItemStruct inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null) _inventoryData.RemoveItem(itemIndex, 1);

            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if (itemAction != null) itemAction.PerformAction(gameObject);
        }

        public void ShowData()
        {
            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                _inventoryUI.UpdateData(item.Key,
                    item.Value.Item.ItemImage,
                    item.Value.Quantity);
            }
        }
    }
}