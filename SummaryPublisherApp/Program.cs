using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryPublisherApp
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
                    NumberofrowsfromthePublisher(connection);
                    var list = Top10Publishers(connection);
                    Console.WriteLine(list.Display());
                var    summary = NumberofbooksforeachpublisherAndPrice(connection);
                    Console.WriteLine(summary.Display());
                    //Console.WriteLine($"{id} - {name}: number of books {count}, total price {price}");

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void NumberofrowsfromthePublisher(SqlConnection connection)
        {
            const string sql = "select count(PublisherId) from Publisher";
            using (var cmd = new SqlCommand(sql, connection))
            {
                var cnt = (int)cmd.ExecuteScalar();
                Console.WriteLine($"publisher count: {cnt}");
            }
        }

        private static PublisherList Top10Publishers(SqlConnection connection)
        {
            const string query = "select top 10 * from Publisher";
            using (var command = new SqlCommand(query, connection))
            {
                Console.WriteLine("Top 10 Publishers:");
                using (var dataReader = command.ExecuteReader())
                {
                    var list = new PublisherList();
                    while (dataReader.Read())
                    {
                        var currentRow = dataReader;
                        list.Add(int.Parse(currentRow["PublisherId"].ToString()), currentRow["Name"].ToString());
                    }

                    return list;
                }
            }
        }

        private static SummaryPublisherList NumberofbooksforeachpublisherAndPrice(SqlConnection connection)
        {
            var query = "select pu.[Name],count(bo.bookid) as Cnt,sum(price) Price from book bo inner join publisher pu on bo.publisherid = pu.publisherid group by pu.publisherid, pu.[name]";
            using (var command = new SqlCommand(query, connection))
            {
                Console.WriteLine("Number of books for each publisher:");
                using (var dataReader = command.ExecuteReader())
                {
                    var list = new SummaryPublisherList();
                    while (dataReader.Read())
                    {
                        var currentRow = dataReader;
                        list.Add(currentRow["Name"].ToString(), int.Parse(currentRow["Cnt"].ToString()), decimal.Parse(currentRow["Price"].ToString()));
                    }
                    return list;
                }
            }
        }
    }
}
