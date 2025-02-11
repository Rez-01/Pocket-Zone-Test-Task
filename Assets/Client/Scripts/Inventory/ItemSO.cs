using UnityEngine;

namespace Inventory
{
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; private set; }

        public int ID => GetInstanceID();

        [field: SerializeField] public int MaxStackSize { get; private set; } = 1;

        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; private set; }

        [field: SerializeField] public Sprite ItemImage { get; private set; }
    }

    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        public string ActionName { get; }
        bool PerformAction(GameObject character);
    }
}