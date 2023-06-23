using CRMSystem.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace CRMSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private string ConnectionString = 
			"Data Source=DESKTOP-POVKKBR\\SQLEXPRESS;Initial Catalog = crm; Integrated Security = True;";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			DataTable table = new DataTable();
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				string query = "select * from Users";
				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
				adapter.Fill(table);	
			}
			return View(table);
		}

        [HttpPost]
        public IActionResult Index(int id, string firstName, string middleName, string lastName, DateTime birthday)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query =
                    "update Users set FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName, Birthday=@Birthday where Id=@Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@MiddleName", middleName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Birthday", birthday);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Orders()
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "select * from Orders";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(table);
            }
            return View(table);
        }

        [HttpPost]
        public IActionResult Orders(int id, int userId, int totalPrice, DateTime dateOfOrder)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query =
                    "update Orders set UserId=@UserId, TotalPrice=@TotalPrice, DateOfOrder=@DateOfOrder where Id=@Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                command.Parameters.AddWithValue("@DateOfOrder", dateOfOrder);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Orders");
        }
    }
}