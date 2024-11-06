using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils;

namespace GamePrototype.Units
{
    public sealed class Inventory
    {
        private readonly uint _capacity;
        private readonly Dictionary<EquipSlot, Item> _equippedItems = new Dictionary<EquipSlot, Item>();
        private readonly List<Item> _items = new List<Item>();
        public IReadOnlyDictionary<EquipSlot, Item> EquippedItems => _equippedItems;
        public IReadOnlyList<Item> Items => _items;

        public Inventory(uint capacity) => _capacity = capacity;

        public bool TryAdd(Item item)
        {
            if (_items.Count == _capacity)
            {
                Console.WriteLine($"Инвентарь заполнен. Предмет {item.Name} не добавлен.");
                return false;
            }

            _items.Add(item);
            Console.WriteLine($"Предмет {item.Name} добавлен в инвентарь.");
            return true;
        }

        public bool TryRemove(Item item)
        {
            if (_items.Count == 0 || !_items.Contains(item))
            {
                Console.WriteLine($"Предмет {item.Name} отсутствует в инвентаре.");
                return false;
            }

            _items.Remove(item);
            Console.WriteLine($"Предмет {item.Name} удалён из инвентаря.");
            return true;
        }

        public bool TryReplaceEquippedItem(EquipSlot slot, Item newItem)
        {
            if (_equippedItems.ContainsKey(slot) && _equippedItems[slot].Equals(newItem))
            {
                Console.WriteLine($"Предмет {newItem.Name} уже находится в слоте {slot}. Замена не произведена.");
                return false;
            }

            if (_items.Count == _capacity)
            {
                Console.WriteLine($"Инвентарь заполнен. Замена невозможна.");
                return false;
            }

            Item oldItem = null;
            if (_equippedItems.TryGetValue(slot, out var existingItem))
            {
                oldItem = existingItem;
                _items.Add(existingItem);
            }

            _equippedItems[slot] = newItem;
            _items.Remove(newItem);

            Console.WriteLine($"Замена произведена. Старый предмет {oldItem?.Name ?? "<пустой>"} заменён на {newItem.Name} в слоте {slot}.");
            return true;
        }
    }
}
