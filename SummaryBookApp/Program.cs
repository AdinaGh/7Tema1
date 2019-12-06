using Data;
using System;
using System.Data.SqlClient;

namespace SummaryBookApp
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
                    Console.WriteLine("2010 Books:");
                    var bookList = Books2010(connection, 2010);
                    Console.WriteLine(bookList.Display());
                    bookList = BookMaxYear(connection, 2000);
                    Console.WriteLine(bookList.Display());
                    bookList = Top10Books(connection);
                    Console.WriteLine(bookList.Display());
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }
        private static BookList Books2010(SqlConnection connection, int year)
        {
            const string query = "select * from Book where Year=@Year";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(new SqlParameter("@Year", year));
                using (var dataReader = command.ExecuteReader())
                {
                    var list = new BookList();
                    while (dataReader.Read())
                    {
                        var currentRow = dataReader;

                        list.Add(int.Parse(currentRow["BookId"].ToString()), currentRow["Title"].ToString(), int.Parse(currentRow["Year"].ToString()), decimal.Parse(currentRow["Price"].ToString()));
                    }
                    return list;
                }
            }
        }

        private static BookList BookMaxYear(SqlConnection connection, int maxYear)
        {
            const string query = "select * from Book where Year<=@MaxYear";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(new SqlParameter("@MaxYear", maxYear));
                Console.WriteLine($"Books with max {maxYear}:");
                using (var dataReader = command.ExecuteReader())
                {
                    var list = new BookList();
                    while (dataReader.Read())
                    {
                        var currentRow = dataReader;

                        var bookId = currentRow["BookId"];
                        var title = currentRow["Title"];
                        var year = currentRow["Year"];
                        var price = currentRow["Price"];

                        list.Add(int.Parse(currentRow["BookId"].ToString()), currentRow["Title"].ToString(),
                            int.Parse(currentRow["Year"].ToString()), decimal.Parse(currentRow["Price"].ToString()));
                    }
                    return list;
                }
            }
        }

        private static BookList Top10Books(SqlConnection connection)
        {
            const string query = "select top 10 * from Book";
            using (var command = new SqlCommand(query, connection))
            {
                Console.WriteLine("Top 10 Books:");
                using (var dataReader = command.ExecuteReader())
                {
                    var list = new BookList();
                    while (dataReader.Read())
                    {
                        var currentRow = dataReader;

                        var title = currentRow["Title"];
                        var year = currentRow["Year"];
                        var price = currentRow["Price"];

                        list.Add(int.Parse(currentRow["BookId"].ToString()), currentRow["Title"].ToString(),
                            int.Parse(currentRow["Year"].ToString()), decimal.Parse(currentRow["Price"].ToString()));
                    }
                    return list;
                }
            }
        }
    }
}