using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonPrefab;

        public void AddButton(string name, Action onClickAction)
        {
            GameObject button = Instantiate(_buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<TMP_Text>().text = name;
        }

        public void Toggle(bool val)
        {
            if (val) RemoveOldButtons();

            gameObject.SetActive(val);
        }

        public void RemoveOldButtons()
        {
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }
    }
}