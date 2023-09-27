using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionDao collectionDao;

        public CollectionController(ICollectionDao _collectionDao)
        {
            collectionDao = _collectionDao;
        }

        [HttpGet]
        public List<Collection> GetCollections()
        {
            return collectionDao.GetCollections();
        }

        [HttpPost]
        public bool CreateNewCollection(Collection collection)
        {
            return collectionDao.CreateNewCollection(collection);
        }

        [HttpDelete]
        public bool DeleteCollection(int collection_id) 
        {
            return collectionDao.DeleteCollection(collection_id);
        }
    }
}
