using System.Collections.Generic;

namespace Capstone.Models
{
    public interface ICollectionDao
    {
        List<Collection> GetCollections();
        bool CreateNewCollection(Collection collection);
    }
}
