using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class ItemSqlDAO : IItemDao
    {
        private readonly string connectionString;
        public ItemSqlDAO(string dbconnectionString)
        {
            connectionString = dbconnectionString;
        }

        public List<Item> GetItemsByCollectionID(int collectionID)
        {
            List<Item> list = new List<Item>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM item WHERE collection_id = @collection_id", conn);
                    cmd.Parameters.AddWithValue("@collection_id", collectionID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Item item = GetItemFromReader(reader);
                        list.Add(item);
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong");
            }

            return list;
        }

        public Item GetItemByItemID(int itemID)
        {
            Item item = new Item();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM item WHERE item_id = @item_id",conn);
                    cmd.Parameters.AddWithValue("@item_id", itemID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        item = GetItemFromReader(reader);
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong");
            }
            return item;
        }

        public bool CreateNewItem(Item item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Generate a new collection ID
                    int newId;
                    using (SqlCommand idCmd = new SqlCommand("SELECT MAX(item_id) FROM item", connection))
                    {
                        object result = idCmd.ExecuteScalar();
                        newId = result == DBNull.Value ? 1 : Convert.ToInt32(result) + 1;
                    }

                    // Insert the new collection with the generated ID
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO item (collection_id, item_id, name, url)" +
                        " VALUES (@collection_id, @item_id, @name, @url);", connection))
                    {
                        cmd.Parameters.AddWithValue("@collection_id", item.Collection_ID);
                        cmd.Parameters.AddWithValue("@item_id", newId);
                        cmd.Parameters.AddWithValue("@name", item.Name);
                        cmd.Parameters.AddWithValue("@url", item.URL);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        bool success = rowsAffected == 1;
                        return success;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteItem(int selectedItemID)
        {
            int rowsAffected;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM item WHERE item_id = @item_id;", conn);
                    cmd.Parameters.AddWithValue("@item_id", selectedItemID);

                    cmd.ExecuteNonQuery();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                bool success = rowsAffected == 1;
                return success;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private Item GetItemFromReader(SqlDataReader reader)
        {
            Item item = new Item();

            item.Collection_ID = Convert.ToInt32(reader["collection_id"]);
            item.Item_ID = Convert.ToInt32(reader["item_id"]);
            item.Name = Convert.ToString(reader["name"]);
            item.URL = Convert.ToString(reader["url"]);

            return item;
        }
    }
}
