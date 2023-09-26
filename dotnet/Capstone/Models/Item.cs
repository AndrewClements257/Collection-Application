namespace Capstone.Models
{
    public class Item
    {
        public int Item_ID { get; set; }
        public int Collection_ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }

        public Item (int item_ID, int collection_ID, string name, string url, string description)
        {
            Item_ID = item_ID;
            Collection_ID = collection_ID;
            Name = name;
            URL = url;
            Description = description;
        }

        public Item ()
        {

        }
    }
}
