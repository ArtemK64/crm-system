using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Interfaces
{
    public class HomeRepository : IHomeRepository
    {
        private readonly IConfiguration _configuration;

        public HomeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetAllOrders()
        {
            DataTable table = new();
            using (SqlConnection connection = new(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                string query = "select * from Orders";
                SqlDataAdapter adapter = new(query, connection);
                adapter.Fill(table);
            }
            return table;
        }

        public DataTable GetAllUsers()
        {
            DataTable table = new();
            using (SqlConnection connection = new(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                string query = "select * from Users";
                SqlDataAdapter adapter = new(query, connection);
                adapter.Fill(table);
            }
            return table;
        }

        public void UpdateOrders(int id, int userId, int totalPrice, DateTime dateOfOrder)
        {
            using (SqlConnection connection = new(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                string query =
                    "update Orders set UserId=@UserId, TotalPrice=@TotalPrice, DateOfOrder=@DateOfOrder where Id=@Id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                command.Parameters.AddWithValue("@DateOfOrder", dateOfOrder);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUsers(int id, string firstName, string middleName, string lastName, DateTime birthday)
        {
            using (SqlConnection connection = new(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                string query =
                    "update Users set FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName, Birthday=@Birthday where Id=@Id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@MiddleName", middleName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Birthday", birthday);
                command.ExecuteNonQuery();
            }
        }
    }
}
