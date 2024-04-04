using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField] private InventoryItem _itemPrefab;
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private InventoryDescription _itemDescription;

        [SerializeField] private ItemActionPanel _actionPanel;

        private List<InventoryItem> _uiItems = new List<InventoryItem>();

        public event Action<int> OnDescriptionRequested,
            OnItemActionRequested;

        private void Awake()
        {
            Hide();
            _itemDescription.ResetDescription();
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                InventoryItem uiItem = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(_contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);

                uiItem.OnItemClicked += HandleItemClick;
                uiItem.OnItemClicked += HandleShowItemActions;

                _uiItems.Add(uiItem);
            }
        }

        public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            _itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            _uiItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (itemIndex < _uiItems.Count)
                _uiItems[itemIndex].SetData(itemImage, itemQuantity);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void Hide()
        {
            _actionPanel.Toggle(false);
            gameObject.SetActive(false);
        }

        public void ResetSelection()
        {
            _itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            _actionPanel.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            _actionPanel.Toggle(true);
            _actionPanel.transform.position = _uiItems[itemIndex].transform.position;
        }

        private void HandleShowItemActions(InventoryItem inventoryItem)
        {
            int index = _uiItems.IndexOf(inventoryItem);
            if (index == -1) return;

            OnItemActionRequested?.Invoke(index);
        }

        private void DeselectAllItems()
        {
            foreach (InventoryItem item in _uiItems)
            {
                item.Deselect();
            }

            _actionPanel.Toggle(false);
        }

        private void HandleItemClick(InventoryItem item)
        {
            int index = _uiItems.IndexOf(item);
            if (index == -1) return;

            OnDescriptionRequested?.Invoke(index);
        }

        public void ResetAllItems()
        {
            foreach (var item in _uiItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}