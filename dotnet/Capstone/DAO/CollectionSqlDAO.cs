using System.Data.SqlClient;
using System.Transactions;
using System;
using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public class CollectionSqlDAO : ICollectionDao
    {
        private readonly string connectionString;
        public CollectionSqlDAO(string dbconnectionString)
        {
            connectionString = dbconnectionString;
        }

        public List<Collection> GetCollections() 
        {
            List<Collection> list = new List<Collection>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString)) 
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM collection", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) 
                    {
                        Collection collection = GetCollectionFromReader(reader);
                        list.Add(collection);
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong");
            }

            return list;
        }

        public bool CreateNewCollection(Collection collection)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Generate a new collection ID
                    int newId;
                    using (SqlCommand idCmd = new SqlCommand("SELECT MAX(collection_id) FROM collection", connection))
                    {
                        object result = idCmd.ExecuteScalar();
                        newId = result == DBNull.Value ? 1 : Convert.ToInt32(result) + 1;
                    }

                    // Insert the new collection with the generated ID
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO collection (collection_id, user_id, name)" +
                        " VALUES (@collection_id, @user_id, @name);", connection))
                    {
                        cmd.Parameters.AddWithValue("@collection_id", newId);
                        cmd.Parameters.AddWithValue("@user_id", collection.User_ID);
                        cmd.Parameters.AddWithValue("@name", collection.Name);

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
        private Collection GetCollectionFromReader(SqlDataReader reader)
        {
            Collection collection = new Collection()
            {
                Collection_ID = Convert.ToInt32(reader["collection_id"]),
                User_ID = Convert.ToInt32(reader["user_id"]),
                Name = Convert.ToString(reader["name"])
            };

            return collection;
        }
    }
}
