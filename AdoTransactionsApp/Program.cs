using System.Data;
using System.Data.SqlClient;

namespace AdoTransactionsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDb;Integrated Security=True;Connect Timeout=10;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = @"INSERT INTO books (title, author, price)
                                            VALUES('Oliver Twist','Charlse Dickens','390.00')";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO books (title, author, price)
                                            VALUES('Prestuplenie i nakazanie','Fedor Dostoevsky','230.00')";
                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
                
            }
        }
    }
}