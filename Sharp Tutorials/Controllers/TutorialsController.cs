using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace Sharp_Tutorials.Controllers
{
    public class TutorialsController : Controller
    {
		static public string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

		[HttpGet]
        public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public string GetMenuList()
		{
			string sqlExpression = "SELECT Id, MenuTitle FROM Tutorial";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				SqlDataReader reader = command.ExecuteReader();
				return (JsonConstructor.GetJson(reader));
			}
		}

		[HttpGet]
		public string GetContent(int id)
		{
			string sqlExpression = "SELECT Title, Text FROM Tutorial WHERE Id ="+id;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				SqlDataReader reader = command.ExecuteReader();
				return (JsonConstructor.GetJson(reader));
			}
		}
	}
}