namespace Capstone.Models
{
    public class Collection
    {
        public string Name { get; set; }
        public int Collection_ID { get; set; }
        public int User_ID { get; set; }

        public Collection(string name, int collection_ID, int user_ID)
        {
            Name = name;
            Collection_ID = collection_ID;
            User_ID = user_ID;
        }

        public Collection() { }
    }
}
