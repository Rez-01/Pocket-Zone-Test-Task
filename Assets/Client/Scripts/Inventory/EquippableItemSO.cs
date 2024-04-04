using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        public bool PerformAction(GameObject character)
        {
            return false;
        }
    }
}