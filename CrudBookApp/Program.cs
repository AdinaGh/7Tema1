using Data;
using System;
using System.Data.SqlClient;

namespace CrudBookApp
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
                    var book = new Book(1, "title11", 2019, 67);
                    AddBook(connection, book);
                    book = new Book(1, "title11-edited", 2017, 100, book.BookId);
                    UpdateBook(connection, book);
                    DeleteBook(connection);
                    Console.WriteLine("Enter id to select: ");
                    var id = Console.ReadLine();
                    book = SelectBook(connection, int.Parse(id));
                    Console.WriteLine(book.Display());
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }

        }

        private static void AddBook(SqlConnection connection, Book book)
        {
            const string sql = @"INSERT INTO Book(Title,Publisherid,Year, Price) Values(@Title,@Publisherid,@Year, @Price);SELECT CAST(scope_identity() AS int);";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@Title", book.Title));
                cmd.Parameters.Add(new SqlParameter("@Publisherid", book.PublisherId));
                cmd.Parameters.Add(new SqlParameter("@Year", book.Year));
                cmd.Parameters.Add(new SqlParameter("@Price", book.Price));
                var newId = (int)cmd.ExecuteScalar();
                book.BookId = newId;
                Console.WriteLine($"new book with id: {newId} was added");
            }
        }

        private static void UpdateBook(SqlConnection connection, Book book)
        {
            Console.WriteLine("Enter id to update: ");
            var id = Console.ReadLine();
            book.BookId = int.Parse(id);
            const string sql = "update Book set Title = @Title,PublisherId = @Publisherid, Year = @Year, Price = @Price where Bookid = @Bookid";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@Title", book.Title));
                cmd.Parameters.Add(new SqlParameter("@Publisherid", book.PublisherId));
                cmd.Parameters.Add(new SqlParameter("@Year", book.Year));
                cmd.Parameters.Add(new SqlParameter("@Price", book.Price));
                cmd.Parameters.Add(new SqlParameter("@Bookid", book.BookId));

                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Updated");
        }

        private static void DeleteBook(SqlConnection connection)
        {
            Console.WriteLine("Enter id to delete: ");
            var id = Console.ReadLine();
            const string command = "delete from Book where BookId = @BookId";
            var param = new SqlParameter("@BookId", id);
            using (var deleteCommand = new SqlCommand(command, connection))
            {
                deleteCommand.Parameters.Add(param);
                deleteCommand.ExecuteNonQuery();
            }
        }

        private static Book SelectBook(SqlConnection connection, int id)
        {
            const string query = "select * from Book where BookId = @BookId";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(new SqlParameter("@BookId", id));
                Console.WriteLine("Selected book:");
                using (var dataReader = command.ExecuteReader())
                {
                    var book = new Book();

                    if (dataReader.Read())
                    {
                        var currentRow = dataReader;
                        book.BookId = id;
                        book.Title = currentRow["Title"].ToString();
                        book.Year = int.Parse(currentRow["Year"].ToString());
                        book.Price = decimal.Parse(currentRow["Price"].ToString());
                    }
                    return book;
                }
            }
        }
    }
}
