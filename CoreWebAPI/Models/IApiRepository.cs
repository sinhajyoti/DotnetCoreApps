using System.Collections.Generic;

namespace CoreWebAPI.Models
{
    public interface IApiRepository
    {
        IEnumerable<Item> GetAll();
        Item GetOne(string itemName);
        IEnumerable<Item> GetByName(string itemName);
        void Add(Item item);
        void Update(Item item);
        Item Delete(string itemName);

    }
}
