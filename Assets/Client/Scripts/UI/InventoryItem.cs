using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _quantityText;
        [SerializeField] private Image _borderImage;
        [SerializeField] private Image _textBG;

        public event Action<InventoryItem> OnItemClicked;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            _itemImage.gameObject.SetActive(false);
            _textBG.gameObject.SetActive(false);
        }

        public void Deselect()
        {
            _borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;

            if (quantity > 1)
            {
                _textBG.gameObject.SetActive(true);
                _quantityText.text = quantity.ToString();
            }
        }

        public void Select()
        {
            _borderImage.enabled = true;
        }

        public void OnPointerClick()
        {
            OnItemClicked?.Invoke(this);
        }
    }
}