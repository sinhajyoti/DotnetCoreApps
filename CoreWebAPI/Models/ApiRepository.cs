using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CoreWebAPI.Models
{
    public class ApiRepository : IApiRepository
    {
        private static ConcurrentDictionary<string, Item> _items = new ConcurrentDictionary<string, Item>();
        public ApiRepository()
        {
            Add(new Item { Name = "Item1", Value = "Value1", IsActive = true });
        }
        public void Add(Item item)
        {
            _items[item.Name] = item;
        }

        public Item Delete(string itemName)
        {
            Item item;
            _items.TryRemove(itemName,out item);
            return item;
        }

        public IEnumerable<Item> GetAll()
        {
            return _items.Values;
        }

        public IEnumerable<Item> GetByName(string itemName)
        {
            throw new NotImplementedException();
        }

        public Item GetOne(string itemName)
        {
            Item item;
            _items.TryGetValue(itemName, out item);
            return item;
        }
        public void Update(Item item)
        {
            _items[item.Name] = item;
        }
    }
}
