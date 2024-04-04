using Inventory;
using UnityEngine;

namespace PickupSystem
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemSO InventoryItem { get; private set; }
        [field: SerializeField] public int Quantity { get; set; } = 1;

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
        }

        public void DestroyItem()
        {
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}