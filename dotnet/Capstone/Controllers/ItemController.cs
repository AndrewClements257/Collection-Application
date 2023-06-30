using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemDao itemDao;

        public ItemController(IItemDao _itemDao)
        {
            itemDao = _itemDao;
        }

        [HttpGet("{collection_id}")]
        public List<Item> GetItemsByID(int collection_id)
        {
            return itemDao.GetItemsByCollectionID(collection_id);
        }

        [HttpPost]
        public bool CreateNewItem(Item item)
        {
            return itemDao.CreateNewItem(item);
        }

        [HttpDelete]
        public bool DeleteItem(int item_id)
        {
            return itemDao.DeleteItem(item_id);
        }

        [HttpPost("upload")]
        public IActionResult UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0]; // Get the uploaded file

                // You can customize the file name and path as per your requirement
                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine("../../vue/src/images", fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream); // Save the file to the specified path
                }

                // Optionally, you can perform additional processing on the uploaded image

                return Ok("Image uploaded successfully");
            }
            catch (System.Exception)
            {
                // Handle any exceptions that occurred during the upload process
                return StatusCode(500, "Error uploading image");
            }
        }

        [HttpDelete("delete/{filename}")]
        public IActionResult DeleteImage(string filename)
        {
            // Set the path to the image file
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "../../vue/src/images", filename);

            if (System.IO.File.Exists(imagePath))
            {
                // Delete the image file
                System.IO.File.Delete(imagePath);

                // Optionally, you can update your database or perform any additional tasks

                return Ok("Image deleted successfully");
            }

            return NotFound("Image not found");
        }
    }
}
