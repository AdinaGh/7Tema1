using Data;
using System;
using System.Data.SqlClient;

namespace InsertPublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(DbSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                    var addPublisher = new Publisher(16, "pub15=6");
                    var pubId = AddPublisher(connection, addPublisher);
                    Console.WriteLine($"new publisher with id: {pubId} was added");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static int AddPublisher(SqlConnection connection, Publisher publisher)
        {
            const string sql = @"INSERT INTO Publisher(Publisherid,Name) Values(@Publisherid, @Name); SELECT CAST(scope_identity() AS int)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@Publisherid", publisher.PublisherId));
                cmd.Parameters.Add(new SqlParameter("@Name", publisher.Name));
                var obj = cmd.ExecuteScalar();
                if (obj != DBNull.Value)
                {
                    publisher.PublisherId = (int)obj;
                }
                var newId = publisher.PublisherId;
                return newId;
            }
        }
    }
}
