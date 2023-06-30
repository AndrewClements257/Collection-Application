using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Models
{
    public interface IItemDao
    {
        List<Item> GetItemsByCollectionID(int collection_id);
        bool CreateNewItem(Item item);
        bool DeleteItem(int item_id);
    }
}