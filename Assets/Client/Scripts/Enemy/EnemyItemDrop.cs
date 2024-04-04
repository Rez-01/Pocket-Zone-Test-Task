using PickupSystem;
using UnityEngine;

namespace Enemy
{
    public class EnemyItemDrop : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private int _itemQuantity;

        private void Awake()
        {
            _item.Quantity = _itemQuantity;
        }

        public void ItemDrop()
        {
            Instantiate(_item, transform.position, Quaternion.identity);
        }
    }
}