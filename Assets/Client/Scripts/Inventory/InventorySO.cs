using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItemStruct> _inventoryItemStructs;

        [field: SerializeField] public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItemStruct>> OnInventoryUpdated;

        public void Initialize()
        {
            _inventoryItemStructs = new List<InventoryItemStruct>();
            for (int i = 0; i < Size; i++)
            {
                _inventoryItemStructs.Add(InventoryItemStruct.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO item, int quantity)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < _inventoryItemStructs.Count;)
                {
                    while (quantity > 0 && HasInventorySpace())
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }

                    InformAboutChange();
                    return quantity;
                }
            }

            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItemStruct newItem = new InventoryItemStruct
            {
                Item = item,
                Quantity = quantity
            };

            for (int i = 0; i < _inventoryItemStructs.Count; i++)
            {
                if (_inventoryItemStructs[i].IsEmpty)
                {
                    _inventoryItemStructs[i] = newItem;
                    return quantity;
                }
            }

            return 0;
        }

        private bool HasInventorySpace()
            => _inventoryItemStructs.Any(item => item.IsEmpty);

        public void AddItem(InventoryItemStruct item)
        {
            AddItem(item.Item, item.Quantity);
        }

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < _inventoryItemStructs.Count; i++)
            {
                if (_inventoryItemStructs[i].IsEmpty) continue;

                if (_inventoryItemStructs[i].Item.ID == item.ID)
                {
                    int maximumAmmount =
                        _inventoryItemStructs[i].Item.MaxStackSize - _inventoryItemStructs[i].Quantity;

                    if (quantity > maximumAmmount)
                    {
                        _inventoryItemStructs[i] = _inventoryItemStructs[i]
                            .ChangeQuantity(_inventoryItemStructs[i].Item.MaxStackSize);
                        quantity -= maximumAmmount;
                    }
                    else
                    {
                        _inventoryItemStructs[i] = _inventoryItemStructs[i]
                            .ChangeQuantity(_inventoryItemStructs[i].Quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while (quantity > 0 && HasInventorySpace())
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }

            return quantity;
        }

        public Dictionary<int, InventoryItemStruct> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItemStruct> returnValue =
                new Dictionary<int, InventoryItemStruct>();

            for (int i = 0; i < _inventoryItemStructs.Count; i++)
            {
                if (_inventoryItemStructs[i].IsEmpty) continue;

                returnValue[i] = _inventoryItemStructs[i];
            }

            return returnValue;
        }

        public InventoryItemStruct GetItemAt(int itemIndex)
        {
            return _inventoryItemStructs[itemIndex];
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (itemIndex < _inventoryItemStructs.Count)
            {
                if (_inventoryItemStructs[itemIndex].IsEmpty) return;

                int remainder = _inventoryItemStructs[itemIndex].Quantity - amount;

                if (remainder <= 0) _inventoryItemStructs[itemIndex] = InventoryItemStruct.GetEmptyItem();
                else
                    _inventoryItemStructs[itemIndex] = _inventoryItemStructs[itemIndex]
                        .ChangeQuantity(remainder);

                InformAboutChange();
            }
        }
    }

    [Serializable]
    public struct InventoryItemStruct
    {
        public int Quantity;
        public ItemSO Item;
        public bool IsEmpty => Item == null;

        public InventoryItemStruct ChangeQuantity(int quantity)
        {
            return new InventoryItemStruct
            {
                Item = this.Item,
                Quantity = quantity
            };
        }

        public static InventoryItemStruct GetEmptyItem()
            => new InventoryItemStruct
            {
                Item = null,
                Quantity = 0
            };
    }
}